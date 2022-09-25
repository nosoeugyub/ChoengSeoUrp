using NSY.Iven;
using UnityEngine;

namespace DM.Building
{
    public enum BuildingType
    { Carrot, House }

    public delegate void GetIGDdelegate(InItemType itemType, int idx);
    public class BuildingObject : MonoBehaviour//, IInteractable
    {
        [SerializeField]
        private Item buildingInfo; //집마다 1개. 필요 재료 타입과 양이 들어있다.
        [SerializeField]
        private RecipeIteminfo[] gotIngredient;
        [SerializeField]
        private IngredientUI[] uiFabs; //생성된 uiFab
        [SerializeField]
        private GameObject clearButton;
        [SerializeField]
        private Sprite finishSprite;
        [SerializeField]
        private Sprite underConstructionSprite;
        [SerializeField]
        private GameObject ingredientUI;

        private bool[] isClear;

        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            gotIngredient = new RecipeIteminfo[buildingInfo.recipe.Length];
            isClear = new bool[buildingInfo.recipe.Length];
        }
        private void OnEnable()
        {
            InitCount();//데이터 저장 시 호출 위치 바꿔야 함. 데이터 초기화 함수임

        }
        public void InitCount()
        {
            for (int i = 0; i < buildingInfo.recipe.Length; i++)
            {
                gotIngredient[i] = new RecipeIteminfo
                {
                    count = 0
                };
                gotIngredient[i].item = buildingInfo.GetIngredientNeededsItemType(i);
            }
        }

        public void InstantiateUIs()
        {
            GetIGDdelegate dg = new GetIGDdelegate(GetIngredient);
            for (int i = 0; i < buildingInfo.recipe.Length; i++)
            {
                uiFabs[i].InitUIs(dg, i);

                string text = string.Format("{0} / {1}", gotIngredient[i].count, buildingInfo.GetNeedCountw(i));
                uiFabs[i].UpdateNowSlashFinishText(text);
            }
        }

        public void GetIngredient(InItemType ingredientype, int idx)
        {
          

            for (int i = 0; i < buildingInfo.recipe.Length; i++)
            {
                if (gotIngredient[i].item.InItemType == ingredientype && !IsTypeClear(i))
                {
                    InventoryNSY ivt = FindObjectOfType<InventoryNSY>();//아이템 넣기
                    //if (ivt.AddItem(gotIngredient[i].item))
                    //{
                    //    print("인벤에 재료가 없어요"); return;
                    //}

                    ++gotIngredient[i].count;
                    //ivt.DeleteItem(gotIngredient[i].item, 1);

                    if (IsTypeClear(i))
                    {
                        isClear[i] = true;
                        if (IsBuildClear())
                        {
                            clearButton.SetActive(true);
                            //추후 클리어 버튼 눌렀을 때 애니 연출 재생을 추가해야 함.
                        }
                    }
                    string text = string.Format("{0} / {1}", gotIngredient[i].count, buildingInfo.GetNeedCountw(i));
                    uiFabs[i].UpdateNowSlashFinishText(text);
                }
            }
        }
        public void ChangeFinishImg()
        {
            spriteRenderer.sprite = finishSprite;
            PlayerData.AddValue(BuildID(), (int)BuildingBehaviorEnum.CompleteBuild, PlayerData.BuildBuildingData, ((int)BuildingBehaviorEnum.length));
        }
        public void StartBuild()
        {
            PlayerData.AddValue(BuildID(), (int)BuildingBehaviorEnum.StartBuild, PlayerData.BuildBuildingData, ((int)BuildingBehaviorEnum.length));
            spriteRenderer.sprite = underConstructionSprite;
            ingredientUI.SetActive(true);
            InstantiateUIs();
        }

        private bool IsBuildClear()
        {
            for (int i = 0; i < isClear.Length; i++)
            {
                if (!isClear[i]) return false;
            }
            return true;
        }
        public int BuildID()
        {
            return (int)buildingInfo.InItemType;
        }
        private bool IsTypeClear(int idx)
        {
            return gotIngredient[idx].count >= buildingInfo.GetNeedCountw(idx);
        }

        public void Interact()
        {
            PlayerData.AddValue(BuildID(), (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
        }

        public void CanInteract()
        {
           // NSY.Player.PlayerInput.OnPressFDown = Interact;
        }

        public void EndInteract()
        {
        }

        public Transform ReturnTF()
        {
            return transform;
        }
    }
}