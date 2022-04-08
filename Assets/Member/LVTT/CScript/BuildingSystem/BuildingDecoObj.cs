using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT.BuildSystem
{
    public class BuildingDecoObj : MonoBehaviour
    {

        //public float xOffset = 1.8f;
        //public float zOffset = 2f;
        //public float yOffset =1f;
        [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
        //LayerMask Mask =  ;
        private Vector3 mOffset;
        private bool itemisSet;

        float MaxX;
        float MinX;
        float MaxZ;
        float MinZ;

        BuildingManager BuildingManager;
        private void Start()
        {
            // BuildingManager = FindObjectOfType<BuildingManager>();
            //HouseBuildAreaCal();
            itemisSet = false;

        }

        private void Update()
        {
            //    if (!itemisSet)
            //{ ItemMove(); }

        }
        //void HouseBuildAreaCal()
        //{
        //    MaxZ = gameObject.transform.position.z + zOffset;
        //    MinZ = gameObject.transform.position.z - zOffset;
        //    MaxX = gameObject.transform.position.x + xOffset;
        //    MinX = gameObject.transform.position.x - xOffset;
        //}
        private void OnMouseDown()
        {
            //mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            //Store offset = gameobject worldpos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseWorldPos();

            switch (itemisSet)
            {
                case true:

                    itemisSet = false;

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

        }
        Vector3 GetMouseWorldPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {

                //return Camera.main.ScreenToWorldPoint(raycastHit.point);
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        void ItemMove()
        {
            Vector3 DragPos = GetMouseWorldPos() + mOffset;
            //if (DragPos.z >= MaxZ)
            //{
            //    DragPos.z = MaxZ;

            //}
            //if (DragPos.z <= MinZ)
            //{
            //    DragPos.z = MinZ;
            //}
            //if (DragPos.x >= MaxX)
            //{
            //    DragPos.x = MaxX;

            //}
            //if (DragPos.x <= MinX)
            //{
            //    DragPos.x = MinX;
            //}
            //DragPos.y = yOffset;
            transform.position = DragPos;
        }

        //private void OnMouseDrag()
        //{
        //    Vector3 DragPos = GetMouseWorldPos() + mOffset;
        //    if (DragPos.z >= MaxZ)
        //    {
        //        DragPos.z = MaxZ;
        //        // transform.position = DragPos;
        //    }
        //    if (DragPos.z <= MinZ)
        //    {
        //        DragPos.z = MinZ;
        //    }
        //    if (DragPos.x >= MaxX)
        //    {
        //        DragPos.x = MaxX;
        //        // transform.position = DragPos;
        //    }
        //    if (DragPos.x <= MinX)
        //    {
        //        DragPos.x = MinX;
        //    }
        //    transform.position = DragPos;
        //}
    }

}