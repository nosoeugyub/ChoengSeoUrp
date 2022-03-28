using UnityEngine;

namespace TT.BuildSystem
{
    public enum BuildItemKind { Wall, Roof, Door, Window }

    public class BuildingItemObj : MonoBehaviour
    {
        public float yDragOffset = 2.5f;
        public float xDragOffset = 2f;
        [SerializeField] public BuildItemKind ItemKind;
        float zOffset;
        private float BuildItemGap = 0.01f;
        [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
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
                       
                        if (this.ItemKind == BuildItemKind.Wall)
                        {}
                        else
                        { BringItemTotheBack(); }
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
                {//Do nothing
                 }
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
            BuildManager.curDragObj = this.GetComponent<BuildingItemObj>();

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

            BuildingBlock CurBlock = BuildManager.nowBuildingBlock;

            if (this.ItemKind == BuildItemKind.Wall)
            {
                DragPos.z = CurBlock.CurWallItemzPos;
            }
            else
            { DragPos.z = CurBlock.CurFrontItemzPos; }

            transform.position = DragPos;
        }

        public void SetBuildItemScale(Vector3 scalenum)
        {
            transform.localScale = scalenum;
        }    

    }

}
