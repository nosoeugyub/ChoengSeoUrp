using DM.Inven;
using UnityEngine;

namespace DM.Building
{
    public enum BuildingType
    { Carrot, House }

    public delegate void GetIGDdelegate(ItemType itemType, int idx);
    public class BuildingObject : MonoBehaviour
    {
        [SerializeField]
        private BuildingInfo buildingInfo; //집마다 1개. 필요 재료 타입과 양이 들어있다.
        [SerializeField]
        private ingredientNeeded[] gotIngredient;
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
            gotIngredient = new ingredientNeeded[buildingInfo.GetIngredientNeededsLength()];
            isClear = new bool[buildingInfo.ingredientNeededs.Length];
        }
        private void OnEnable()
        {
            InitCount();//데이터 저장 시 호출 위치 바꿔야 함. 데이터 초기화 함수임
            
        }
        public void InitCount()
        {
            for (int i = 0; i < buildingInfo.ingredientNeededs.Length; i++)
            {
                gotIngredient[i] = new ingredientNeeded
                {
                    count = 0
                };
                gotIngredient[i].item = buildingInfo.GetIngredientNeededsItemType(i);
            }
        }

        public void InstantiateUIs()
        {
            GetIGDdelegate dg = new GetIGDdelegate(GetIngredient);
            for (int i = 0; i < buildingInfo.ingredientNeededs.Length; i++)
            {
                uiFabs[i].InitUIs(dg, i);

                string text = string.Format("{0} / {1}", gotIngredient[i].count, buildingInfo.GetNeedCountw(i));
                uiFabs[i].UpdateNowSlashFinishText(text);
            }
        }

        public void GetIngredient(ItemType ingredientype, int idx)
        {
            Debug.Log("GetIngredient " + ingredientype);

            for (int i = 0; i < buildingInfo.ingredientNeededs.Length; i++)
            {
                if (gotIngredient[i].item.ItemType == ingredientype && !IsTypeClear(i))
                {
                    InventoryManager ivt = FindObjectOfType<InventoryManager>();
                    if (ivt.GetItemValue(gotIngredient[i].item) <= 0)
                    { print("인벤에 재료가 없어요"); return; }

                    ++gotIngredient[i].count;
                    ivt.DeleteItem(gotIngredient[i].item, 1);

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
        }
        public void StartBuild()
        {
            //플레이어 데이터에 해당 빌딩이 없다면 추가
            //if (!PlayerData.BuildBuildingData.ContainsKey(buildingInfo.BuildingID()))
            //{
            //    PlayerData.BuildBuildingData.Add(buildingInfo.BuildingID(), new BuildingBehavior());
            //}
            PlayerData.AddValueInDictionary(BuildID(),2, PlayerData.BuildBuildingData);
            //PlayerData.BuildBuildingData[BuildID()].amounts[2]++;//아니 건물의 인덱스가 필요한데
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
            return buildingInfo.BuildingID();
        }
        private bool IsTypeClear(int idx)
        {
            return gotIngredient[idx].count >= buildingInfo.GetNeedCountw(idx);
        }
    }
}