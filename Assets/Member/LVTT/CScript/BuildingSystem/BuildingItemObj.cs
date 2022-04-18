using UnityEngine;

namespace TT.BuildSystem
{

    public class BuildingItemObj : ItemObject, IBuildable
    {
        [SerializeField] public BuildItemKind ItemKind;
        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;

        public float MaxScale = 1.5f;
        public float MinScale = 0.3f;
        public int breakCount;

        [SerializeField] private bool itemisSet;
        [SerializeField] private bool isFirstDrop;

        float MaxX;
        float MinX;
        float MaxY;
        float MinY;

        Vector3 ObjOriginPos;

        BuildingBlock parentBuildArea;

        public bool IsFirstDrop
        {
            get
            {
                return isFirstDrop;
            }
            set
            {
                isFirstDrop = value;
            }
        }
        public bool ItemisSet
        {
            get
            {
                return itemisSet;
            }
            set
            {
                itemisSet = value;
            }
        }
        private new void Awake()
        {
            base.Awake();
            itemisSet = false;
            isFirstDrop = true;
        }
        private void Update()
        {
            if (!itemisSet)
            {
                ItemMove();
            }
        }
        /// <summary>
        /////////////////////////Update
        private void ItemMove()
        {
            var movePos = Input.mousePosition;
            movePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            movePos = Camera.main.ScreenToWorldPoint(movePos);
            HouseBuildAreaCal();
            if (movePos.x >= MaxX) movePos.x = MaxX;
            if (movePos.x <= MinX) movePos.x = MinX;
            if (movePos.y >= MaxY) movePos.y = MaxY;
            if (movePos.y <= MinY) movePos.y = MinY;

            transform.position = movePos;
        }
        void HouseBuildAreaCal()
        {
            MaxX = ObjOriginPos.x + parentBuildArea.areaWidthsize / 2 - (quad.transform.localScale.x * transform.localScale.x) / 2;
            MinX = ObjOriginPos.x - parentBuildArea.areaWidthsize / 2 + (quad.transform.localScale.x * transform.localScale.x) / 2;
            MaxY = ObjOriginPos.y + parentBuildArea.areaHeightsize / 2 - quad.transform.localScale.y * transform.localScale.y / 2;
            MinY = ObjOriginPos.y - parentBuildArea.areaHeightsize / 2 + quad.transform.localScale.y * transform.localScale.y / 2;
        }
        /// 
        /// </summary>

        public void SetParentBuildArea(BuildingBlock pb)
        {
            //Debug.Log("SetParent");
            parentBuildArea = pb;
            parentBuildArea.curInteractObj = this;
            ObjOriginPos = gameObject.transform.position;
        }
        public void SetBuildItemScale(Vector3 scalenum)
        {
            if (scalenum.x >= MaxScale) scalenum.x = MaxScale;
            if (scalenum.x <= MinScale) scalenum.x = MinScale;
            if (scalenum.y >= MaxScale) scalenum.y = MaxScale;
            if (scalenum.y <= MinScale) scalenum.y = MinScale;
            transform.localScale = scalenum;
        }

        public string CanInteract()
        {
            return "BuildItemObj";
        }
        public void Demolish()
        {
            breakCount--;
            Debug.Log("뚜가" + breakCount);
            //이펙트 등
            if (breakCount == 0)
            {
                Debug.Log("파괴");
                if (item.OutItemType == OutItemType.BuildWall)
                    parentBuildArea.hasWall = false;
                else if (item.OutItemType == OutItemType.BuildSign)
                    parentBuildArea.hasSign = false;
                //파괴 임시 처리
                parentBuildArea.RemoveBuildItemToList(gameObject);
                Destroy(gameObject);
            }
        }
    }

}
