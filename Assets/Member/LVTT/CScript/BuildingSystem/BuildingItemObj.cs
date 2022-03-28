using UnityEngine;

namespace TT.BuildSystem
{
    public enum BuildItemKind { Wall, Roof, Door, Window, Signboard, Etc }

    public class BuildingItemObj : ItemObject, IBuildable
    {
        [SerializeField] public BuildItemKind ItemKind;
        [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
        [SerializeField] InItemType toolType;

        [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

        public float yDragOffset = 2.5f;
        public float xDragOffset = 2f;

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

        BuildingManager BuildManager;
        private void Awake()
        {
            BuildManager = FindObjectOfType<BuildingManager>();
            HouseBuildAreaCal();
            itemisSet = false;
            canTouch = false;
            zOffset = transform.position.z;
        }

        private void Update()
        {


            if (!itemisSet)
            {
                ItemMove();
            }


        }
        void HouseBuildAreaCal()
        {
            MaxX = gameObject.transform.position.x + xDragOffset;
            MinX = gameObject.transform.position.x - xDragOffset;
            MaxY = gameObject.transform.position.y + yDragOffset;
            MinY = gameObject.transform.position.y - yDragOffset;
        }
        private void OnMouseDown()
        {
            //mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            //Store offset = gameobject worldpos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            switch (itemisSet)
            {
                case true:
                    if (canTouch)
                    {
                        BringItemTotheBack();
                        itemisSet = false;
                        print("itemisSet = false");
                    }
                    break;
                case false:
                    SetItemPos();
                    break;
            }
        }

        void SetItemPos()
        {
            transform.position = GetMouseWorldPos() + mOffset;
            itemisSet = true;
            canTouch = false;
            print("itemisSet = true, canTouch = false");
            BuildManager.OnBuildItemDrag = false;
        }

        Vector3 GetMouseWorldPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {
                if (raycastHit.collider.gameObject)
                {

                    if (!BuildManager.OnBuildItemDrag)
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
            BuildingBlock CurBlock = BuildManager.nowBuildingBlock;
            foreach (GameObject item in CurBlock.BuildItemList)
            {
                BuildingItemObj ItemObj = item.GetComponent<BuildingItemObj>();
                if (ItemObj.ItemKind == BuildItemKind.Wall)
                { }
                else
                {

                    Vector3 MoveBackPos = item.transform.position;
                    MoveBackPos.z = item.transform.position.z + BuildItemGap;
                    if (MoveBackPos.z <= CurBlock.MaxBackItemzPos)
                    {
                        MoveBackPos.z = CurBlock.MaxBackItemzPos;
                    }
                    item.transform.position = MoveBackPos;
                }

            }
        }
        void ItemMove()
        {

            BuildManager.OnBuildItemDrag = true;
           // print("BuildManager.OnBuildItemDrag = true");

            Vector3 DragPos = GetMouseWorldPos() + mOffset;

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

            //DragPos.z = 502.6f;
            BuildingBlock CurBlock = BuildManager.nowBuildingBlock;
            DragPos.z = CurBlock.CurFrontItemzPos;

            transform.position = DragPos;
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
