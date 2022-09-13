using UnityEngine;

namespace DM.Building
{
    public class BuildingHandyObjSpawn : MonoBehaviour
    {
        public Transform PlayerPos;
        public BuildingItemObj curInteractHandyObj;

        //Essential build item ground Y Pos
        //탁 Y 값이 한 가지만 사용하면 설치하는 거 너무 힘들어서 대신에 Y 값 범위 사용했다 
        float curInteractHandyObjGroundMaxYPos = 20f;
        float curInteractHandyObjGroundMinYPos = 19.4f;
        //
        public BuildingItemObj curMouseOverEssentialObj;
        RaycastHit hit;
        Ray ray;
        int layerMask_Ground;
        int layerMask_Interactable;

        private void Start()
        {
            layerMask_Ground = 1 << LayerMask.NameToLayer("Ground");
            layerMask_Interactable = 1 << LayerMask.NameToLayer("Interactable");
        }
        private void Update()
        {


            //MouseOver Essential item 확인
            //if (Physics.Raycast(ray, out hit, 10000, layerMask)) //땅 검출
            {
                //if (hit.collider.GetComponent<BuildingItemObj>() != null)
                //{
                //    if (hit.collider.GetComponent<BuildingItemObj>().toolType == InItemType.BuildingItemObj_Essential)
                //    {
                //        curMouseOverEssentialObj = hit.collider.GetComponent<BuildingItemObj>();
                //        Debug.Log("Mouse is over EssentialObj");
                //    }
                //    else if (hit.collider.GetComponent<BuildingItemObj>().toolType == InItemType.BuildingItemObj_Additional)
                //    {

                //        Debug.Log("Mouse is over AdditionalObj");
                //    }

                //}
                //else
                //{
                //    curMouseOverEssentialObj = null;
                //}
            }
            if (curInteractHandyObj && !curInteractHandyObj.ItemisSet)
                ItemMove();
            //건축모드처럼 간이건축 설치 method
            if (Input.GetMouseButtonDown(0))
            {
                //print("MouseDown");
                //if (curInteractHandyObj == null) return;

                //if (curInteractHandyObj.GetItem().InItemType == InItemType.BuildingItemObj_Essential)
                //{
                //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                //    if (Physics.Raycast(ray, out hit, 100, layerMask_Ground))
                //    {
                //        SetEssentialItemObj();
                //    }

                //}
                //else if (curInteractHandyObj.GetItem().InItemType == InItemType.BuildingItemObj_Additional)
                //{
                //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                //    if (Physics.Raycast(ray, out hit, 100, layerMask_Interactable))
                //    {
                //        if (hit.collider.GetComponent<BuildingItemObj>().GetItem().InItemType == InItemType.BuildingItemObj_Essential)
                //        {
                //            //SetAdditionalItemObj();
                //        }
                //    }
                //}



                //if (Physics.Raycast(ray, out hit, 100))
                //{

                //    // print("Hit object" + hit.collider.name);

                //    if (hit.collider.GetComponent<BuildingItemObj>() == null) //자재가 아닌걸 클릭 시
                //    {
                //        if (!curInteractHandyObj.ItemisSet && !curInteractHandyObj.IsFirstDrop)
                //        {
                //            if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Essential)
                //            {
                //                if (curInteractHandyObj.transform.position.y <= curInteractHandyObjGroundMaxYPos && curInteractHandyObj.transform.position.y >= curInteractHandyObjGroundMinYPos)
                //                {
                //                    print("ItemisSet = true 1 ");
                //                    SetEssentialItemObj();
                //                }
                //            }
                //            else if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Additional)
                //            {
                //                print("AdditionalItemisSet = true 2 ");
                //                SetAdditionalItemObj();
                //            }


                //        }
                //        else //처음 생성 시
                //        {
                //            print("ItemisSet = false 1");
                //        }

                //        return;
                //    }

                //    if (curInteractHandyObj.ItemisSet) //자재 클릭 + 세팅된 자재일 때
                //    {
                //        print("ItemisSet = false 2");
                //        curInteractHandyObj = hit.collider.GetComponent<BuildingItemObj>();
                //        curInteractHandyObj.ItemisSet = false;
                //    }
                //    else //자재 클릭 + 무빙중일 때
                //    {
                //        if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Essential)
                //        {
                //            if (curInteractHandyObj.transform.position.y <= curInteractHandyObjGroundMaxYPos && curInteractHandyObj.transform.position.y >= curInteractHandyObjGroundMinYPos)
                //            {
                //                print("EssentialItemisSet = true 2 ");
                //                SetEssentialItemObj();

                //            }
                //        }
                //        else if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Additional)
                //        {
                //            print("AdditionalItemisSet = true 2 ");
                //            SetAdditionalItemObj();
                //        }


                //    }
                //}
                //else
                //{
                //    print("ItemisSet = true 3 ");

                //}
            }
        }
        private void ItemMove()
        {
            if (curInteractHandyObj.GetItem().InItemType == InItemType.BuildingItemObj_Essential)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Physics.Raycast(ray, out hit, 10000, layerMask_Ground)) //땅검출
                {
                    curInteractHandyObj.transform.position = hit.point;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (curInteractHandyObj.IsFirstDrop)
                    {
                        print("IsFirstDrop " + hit.collider.name);
                        curInteractHandyObj.IsFirstDrop = false;
                    }
                    else
                    {
                        curInteractHandyObj.ItemisSet = true;
                        SetEssentialItemObj();
                        print("Hit object " + hit.collider.name);
                    }
                }

            }
            else if (curInteractHandyObj.GetItem().InItemType == InItemType.BuildingItemObj_Additional)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

                if (Physics.Raycast(ray, out hit, 10000, layerMask_Interactable)) //상호작용검출
                {
                    if (hit.collider.GetComponent<BuildingItemObj>().GetItem().InItemType == InItemType.BuildingItemObj_Essential)
                    {
                        curInteractHandyObj.transform.position = hit.point + curInteractHandyObj.transform.forward * -0.01f;
                        print("Hit object " + hit.collider.name);
                        if (Input.GetMouseButtonDown(0))
                        {
                            //if (curInteractHandyObj.IsFirstDrop)
                            //{
                            //    print("IsFirstDrop " + hit.collider.name);
                            //    curInteractHandyObj.IsFirstDrop = false;
                            //}
                            //else
                            {
                                curInteractHandyObj.ItemisSet = true;
                                SetAdditionalItemObj(hit.transform);
                            }
                        }
                    }
                }

            }
        }

        private void SetEssentialItemObj()
        {
            curInteractHandyObj.ItemisSet = true;
        }

        private void SetAdditionalItemObj(Transform parent)
        {
            //if (curMouseOverEssentialObj != null)
            {
                curInteractHandyObj.ItemisSet = true;
                curInteractHandyObj.transform.parent = parent;// curMouseOverEssentialObj.transform;
                Vector3 SetAdditionalObjPos = curInteractHandyObj.transform.localPosition;
                SetAdditionalObjPos.z = -0.1f;
                curInteractHandyObj.transform.localPosition = SetAdditionalObjPos;

            }
        }

        public void HandySpawnBuildItem(Item spawnObj)
        {
            Vector3 spawnPos = PlayerPos.position;
            GameObject newPrefab = Instantiate(spawnObj.ItemPrefab, spawnPos, Quaternion.identity, transform);
            newPrefab.name = spawnObj.name;
            curInteractHandyObj = newPrefab.gameObject.GetComponent<BuildingItemObj>();


        }

        public float DistanceFromCurEssentialObjTo(Vector3 movePos)
        {
            Vector3 VecY = new Vector3(curMouseOverEssentialObj.transform.position.x, 0, curMouseOverEssentialObj.transform.position.z);
            Vector3 moveposY = new Vector3(movePos.x, 0, movePos.z);
            return Vector3.Distance(moveposY, VecY);

        }
        public float DistanceFromCharacterTo(Vector3 movePos)
        {
            Vector3 VecY = new Vector3(PlayerPos.transform.position.x, 0, PlayerPos.transform.position.z);
            Vector3 moveposY = new Vector3(movePos.x, 0, movePos.z);
            return Vector3.Distance(moveposY, VecY);

        }
    }
}

