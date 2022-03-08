using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.BuildSystem
{
    public class BuildingItemDrag : MonoBehaviour
    {
        float yOffset=2f;
        float xOffset=1.8f;
        private Vector3 mOffset;
        private float mZCoord;
        float MaxX;
        float MinX;
        float MaxY;
        float MinY;

        private void Start()
        {
            HouseBuildAreaCal();   
        }

        void HouseBuildAreaCal()
        {
            MaxX = gameObject.transform.position.x + xOffset;
            MinX= gameObject.transform.position.x - xOffset;
            MaxY = gameObject.transform.position.y + yOffset;
            MinY = gameObject.transform.position.y - yOffset;
        }    
        private void OnMouseDown()
        {
           
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            //Store offset = gameobject worldpos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }

        Vector3 GetMouseWorldPos()
        {
            //pixel coordinates (x,y)
            Vector3 mousePoint = Input.mousePosition;

            //z coordinate of game object on screen
            mousePoint.z = mZCoord;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void OnMouseDrag()
        {
           Vector3 DragPos= GetMouseWorldPos() + mOffset;
   
            if(DragPos.x>=MaxX)
            {
                DragPos.x = MaxX;
               // transform.position = DragPos;
            }
            if(DragPos.x <= MinX)
            {
                DragPos.x = MinX;
            }
            if (DragPos.y >= MaxY)
            {
                DragPos.y = MaxY;
                // transform.position = DragPos;
            }
            if (DragPos.y <= MinY)
            {
                DragPos.y = MinY;
            }
            transform.position = DragPos;
            //else
            //{
            //    transform.position = DragPos;
            //}    
            // transform.position= GetMouseWorldPos() + mOffset;
        }
    }

}
