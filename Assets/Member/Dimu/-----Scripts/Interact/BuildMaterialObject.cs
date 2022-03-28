//using UnityEngine;
//using TT.BuildSystem;

//public class BuildMaterialObject : MonoBehaviour, IInteractble
//{
//    [SerializeField]public BuildMaterialState buildMaterialState;//지붕인지 간판인지...
//    [SerializeField] int buildMatId;

//    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
//    [SerializeField] InItemType toolType;
//    //BuildingBlock BuildBlock;
//    void Start()
//    {
//        //BuildBlock = GetComponent<BuildingBlock>();
//    }
//    public string CanInteract()
//    {
//        return "건축자재 상호작용";
//    }
//    public void Interact()
//    {
//        //PlayerData.AddValue(buildingId, (int)BuildingBehaviorEnum.Interact, PlayerData.BuildBuildingData, (int)BuildingBehaviorEnum.length);
//    }
//    public Transform ReturnTF()
//    {
//        return transform;
//    }

//    public void Demolish()
//    {

//    }
//}
//public enum BuildMaterialState
//{ Wall, Signboard, Etc }