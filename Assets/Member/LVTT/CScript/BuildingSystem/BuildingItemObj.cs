using UnityEngine;

namespace TT.BuildSystem
{

    public class BuildingItemObj : ItemObject, IBuildable, IDropable
    {
        [SerializeField] BuildObjAttribute attributes;

        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;

        [SerializeField] float MaxScale = 1.5f;
        [SerializeField] float MinScale = 0.3f;
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
                DropItems();
                parentBuildArea.RemoveBuildItemToList(gameObject);
                Destroy(gameObject);
            }
        }
        public void DropItems()
        {
            GameObject instantiateItem;
            foreach (DropItem item in item.DropItems)
            {
                //print("spawn" + 2);
                for (int i = 0; i < item.count; ++i)
                {
                    instantiateItem = Instantiate(item.itemObj) as GameObject;
                    Vector3 randVec = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                    instantiateItem.transform.position = gameObject.transform.position + randVec;
                    //print("spawn" + instantiateItem.name);
                }
            }
        }

        public void PutDownBuildingItemObj(float areaWidthSize, float areaHeightSize)
        {
            //위치 저장
            //좌중우

            if (transform.localPosition.x >= (areaWidthSize / (int)BuildHPos.Length) * 0 - (areaWidthSize / 2)
                && transform.localPosition.x <= areaWidthSize / (int)BuildHPos.Length * 1 - (areaWidthSize / 2))
                attributes.buildHPos = BuildHPos.Left;
            else if (transform.localPosition.x > (areaWidthSize / (int)BuildHPos.Length) * 1 - (areaWidthSize / 2)
                && transform.localPosition.x < areaWidthSize / (int)BuildHPos.Length * 2 - (areaWidthSize / 2))
                attributes.buildHPos = BuildHPos.Mid;
            else
                attributes.buildHPos = BuildHPos.Right;

            //하중상
            if (transform.localPosition.y >= (areaHeightSize / (int)BuildHPos.Length) * 0  //생략 가능할듯
                && transform.localPosition.y <= areaHeightSize / (int)BuildHPos.Length * 1)
                attributes.buildVPos = BuildVPos.Bottom;
            else if (transform.localPosition.y > (areaHeightSize / (int)BuildHPos.Length) * 1
                && transform.localPosition.y < areaHeightSize / (int)BuildHPos.Length * 2)
                attributes.buildVPos = BuildVPos.Mid;
            else
                attributes.buildVPos = BuildVPos.Top;

            print(attributes.buildHPos.ToString());
            print(attributes.buildVPos.ToString());

            //크기 저장
            float scaleRange = MaxScale - MinScale; // 1.5 0.3     1.2

            if (transform.localScale.x >= MinScale  //생략 가능할듯 //0.3
                && transform.localScale.x <= scaleRange / (int)BuildSize.Length * 1 + MinScale) //0.4 + 0.3  0.7
                attributes.buildSize = BuildSize.Small;
            else if (transform.localScale.x > (scaleRange / (int)BuildSize.Length) * 1 + MinScale //0.7
                && transform.localScale.x < scaleRange / (int)BuildSize.Length * 2 + MinScale)    //1.1
                attributes.buildSize = BuildSize.Normal;
            else
                attributes.buildSize = BuildSize.Big;

            print(attributes.buildSize.ToString());

        }
    }

}
