using System.Collections.Generic;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildingBlock : MonoBehaviour, IInteractble
    {
        BuildingManager BuildManager;

        public Transform HouseBuild;
        public List<GameObject> BuildItemList;
        [HideInInspector]
        public float CurWallItemzPos;
        [HideInInspector]
        public float CurFrontItemzPos;
        [HideInInspector]
        public float MaxBackItemzPos;

        [SerializeField] public BuildState buildState;
        [SerializeField] int buildingId;
        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;
        [HideInInspector]
        public bool buildButtonFuncAdded;
        [HideInInspector]
        public bool hasWall = false;
        internal bool hasSign =  false;

        ////////////////////////////////////////////////////////
        void Start()
        {
            BuildManager = FindObjectOfType<BuildingManager>();
            buildButtonFuncAdded = false;
            //hasWall = false;
        }

        public void AddBuildItemToList(GameObject Item)
        {
            BuildItemList.Add(Item);
        }
        public void RemoveDemolishedBuildItem()
        {
            foreach (GameObject Item in BuildItemList)
            {
                if (Item == null)
                {
                    this.BuildItemList.Remove(Item);
                }
            }
        }

        void ClearBuildItemList(BuildingBlock buildingBlock)
        {
            buildingBlock.BuildItemList.Clear();
        }
        public void OnBuildMode(UnityEngine.UI.Button[] buttons, GameObject interactUI)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }

            //건축물 상호작용 인덱스 체크
            Interact();
            //Set Event Methods
            if (buildState == BuildState.NotFinish)
            {
                if (!this.buildButtonFuncAdded)
                {
                    buttons[0].onClick.AddListener(() =>
                    {
                        BuildManager.BuildModeOn(this, buttons, interactUI);
                        print("1. Build Building");
                        //1. Build Building
                    });
                    buttons[1].onClick.AddListener(() =>
                    {
                        BuildManager.BuildDemolishModeOn(this, buttons, interactUI);
                        //ClearBuildItemList(this);
                        print("2. break Building");
                        //2. break Building
                    });
                    buttons[2].onClick.AddListener(() =>
                    {
                        //CompleteBuilding();
                        print("3. Finish Building");
                        //3. Finish Building
                    });
                    this.buildButtonFuncAdded = true;
                }

            }
            else if (buildState == BuildState.Finish)
            {
                if (!this.buildButtonFuncAdded)
                {
                    buttons[0].onClick.AddListener(() =>
                    {
                        //BuildBuilding();
                        print("1. Repair Building");
                        //1. Repair Building
                    });
                    buttons[1].onClick.AddListener(() =>
                    {
                        //DemolishBuidling();
                        print("2. break Building");
                        //2. break Building
                    });
                    buttons[2].gameObject.SetActive(false);
                    this.buildButtonFuncAdded = true;
                }

            }
        }
        ////////////////////////////////////////////////////////
        public void Interact()
        {
            PlayerData.AddValue(buildingId, (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
        }
        public string CanInteract()
        {
            return "건물 상호작용";
        }

        public Transform ReturnTF()
        {
            return transform;
        }
        public void SetBuildingState(BuildState buildstate)
        {
            buildState = buildstate;
        }

        void BuildButtonsListenerRemove(UnityEngine.UI.Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}



