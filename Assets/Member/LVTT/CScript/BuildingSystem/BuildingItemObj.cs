using UnityEngine;

namespace TT.BuildSystem
{

    public class BuildingItemObj : ItemObject, IBuildable
    {
        [SerializeField] public BuildItemKind ItemKind;
        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;

        [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

        public float yDragOffset = 2.5f;
        public float xDragOffset = 2f;
        public float MaxScale;
        public float MinScale;
        public int breakPoint;

        float zOffset;
        private float BuildItemGap = 0.01f;
        private Vector3 mOffset;
        private float mZCoord;
        private bool itemisSet;
        bool canTouch;


        float MaxX;
        float MinX;
        float MaxY;
        float MinY;

        Vector3 ObjOriginPos;
        float ObjOriginWidth;
        float ObjOriginHeight;
        float curObjWidth;
        float curObjHeight;

        BuildingBlock parentBuildArea;
        private void Awake()
        {
            //parentBuildArea = FindObjectOfType<BuildingBlock>();
            itemisSet = false;
            canTouch = false;
            zOffset = transform.position.z;
        }
        public void SetParentBuildArea(BuildingBlock pb)
        {
            Debug.Log("SetParent");
            parentBuildArea = pb;
            HouseBuildAreaCal();
        }
        private void Update()
        {
            Debug.Log("ItemisSet = " + itemisSet + " || canTouch = " + canTouch);
            if (parentBuildArea.CurBuildMode == BuildMode.BuildHouseMode)
            {
                if (!itemisSet)
                {
                    ItemMove();
                }
            }

        }
        void HouseBuildAreaCal()
        {
            ObjOriginPos = gameObject.transform.position;
            MaxX = gameObject.transform.position.x + xDragOffset;
            MinX = gameObject.transform.position.x - xDragOffset;
            MaxY = gameObject.transform.position.y + yDragOffset;
            MinY = gameObject.transform.position.y - yDragOffset;
            ObjOriginWidth = parentBuildArea.HalfGuideObjWidth - xDragOffset;
            ObjOriginHeight = parentBuildArea.HalfGuideObjHeight - yDragOffset;
            curObjWidth = ObjOriginWidth;
            curObjHeight = ObjOriginHeight;

        }
        private void OnMouseDown() //마우스를 눌렸을 때
        {
            print("OnMouseDown");
            if (BuildingBlock.isBuildMode)
            {
                mOffset = gameObject.transform.position - GetMouseWorldPos();
                switch (itemisSet)
                {
                    case true://아이템이 세팅되어 있는가?
                        if (canTouch)
                        {
                            if (this.ItemKind == BuildItemKind.Wall)
                            { }
                            else
                            { BringItemTotheBack(); }
                            itemisSet = false;
                        }
                        break;
                    case false://아이템을 마우스가 들고 있는가?
                        SetItemPos();//자리에 세팅
                        break;
                }
            }
            else if (parentBuildArea.CurBuildMode == BuildMode.DemolishMode)
            {
                mOffset = gameObject.transform.position - GetMouseWorldPos();
                if (canTouch)
                {
                    breakPoint -= 1;
                    if (breakPoint <= 0)
                    { DemolishBuildItem(); }
                }
            }

        }

        void SetItemPos()
        {
            transform.position = GetMouseWorldPos() + mOffset;
            itemisSet = true;
            canTouch = false;
            print("itemisSet = true, canTouch = false");
            parentBuildArea.OnBuildItemDrag = false;
        }
        void DemolishBuildItem()
        {
            Destroy(gameObject);
        }
        Vector3 GetMouseWorldPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {
                if (raycastHit.collider.gameObject)
                {

                    if (!parentBuildArea.OnBuildItemDrag)
                    {
                        canTouch = true;
                        print("canTouch = true");
                    }
                }
                //return Camera.main.ScreenToWorldPoint(raycastHit.point);
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        void BringItemTotheBack()
        {
            foreach (GameObject item in parentBuildArea.BuildItemList)
            {
                BuildingItemObj ItemObj = item.GetComponent<BuildingItemObj>();
                if (ItemObj.ItemKind == BuildItemKind.Wall)
                {//Do nothing
                }
                else
                {

                    Vector3 MoveBackPos = item.transform.position;
                    MoveBackPos.z = item.transform.position.z + BuildItemGap;
                    if (MoveBackPos.z <= parentBuildArea.MaxBackItemzPos)
                    {
                        MoveBackPos.z = parentBuildArea.MaxBackItemzPos;
                    }
                    item.transform.position = MoveBackPos;
                }

            }
        }
        void ItemMove()
        {

            parentBuildArea.OnBuildItemDrag = true;
            //print("BuildManager.OnBuildItemDrag = true");
            parentBuildArea.curDragObj = this.GetComponent<BuildingItemObj>();
            Vector3 DragPos = GetMouseWorldPos() + mOffset;
            HouseBuildReCal();
            if (DragPos.x >= MaxX)
            {
                DragPos.x = MaxX;

            }
            if (DragPos.x <= MinX)
            {
                DragPos.x = MinX;
            }
            if (DragPos.y >= MaxY)
            {
                DragPos.y = MaxY;

            }
            if (DragPos.y <= MinY)
            {
                DragPos.y = MinY;
            }


            if (this.ItemKind == BuildItemKind.Wall)
            {
                //DragPos.z = CurBlock.CurWallItemzPos;
            }
            else
            { DragPos.z = parentBuildArea.CurFrontItemzPos; }

            transform.position = DragPos;
        }
        public void SetBuildItemScale(Vector3 scalenum)
        {
            if (scalenum.x >= MaxScale)
            {
                scalenum.x = MaxScale;
            }
            if (scalenum.x <= MinScale)
            {
                scalenum.x = MinScale;
            }
            if (scalenum.y >= MaxScale)
            {
                scalenum.y = MaxScale;
            }
            if (scalenum.y <= MinScale)
            {
                scalenum.y = MinScale;
            }
            transform.localScale = scalenum;
            curObjWidth = ObjOriginWidth * transform.localScale.x;
            //Debug.Log("curobjWidth = " + curObjWidth);

            curObjHeight = ObjOriginHeight * transform.localScale.y;

        }

        void HouseBuildReCal()
        {
            //Debug.Log("MaX = " + MaxX);
            float tempMaxX = ObjOriginPos.x + (parentBuildArea.HalfGuideObjWidth - curObjWidth);
            //Debug.Log("ObjoriginX" + ObjOriginPos.x);
            //Debug.Log("HalfGuideWidth" + BuildManager.HalfGuideObjWidth);
            //Debug.Log("TempMax=" + tempMaxX);
            MaxX = ObjOriginPos.x + (parentBuildArea.HalfGuideObjWidth - curObjWidth);
            //Debug.Log(MaxX);
            MinX = ObjOriginPos.x - (parentBuildArea.HalfGuideObjWidth - curObjWidth);
            //Debug.Log(MinX);
            MaxY = ObjOriginPos.y + (parentBuildArea.HalfGuideObjHeight - curObjHeight);
            MinY = ObjOriginPos.y - (parentBuildArea.HalfGuideObjHeight - curObjHeight);
        }
        public string CanInteract()
        {
            return "BuildItemObj";
        }

        public Transform ReturnTF()
        {
            return transform;
        }

        public void Demolish()
        {

        }

    }

}
