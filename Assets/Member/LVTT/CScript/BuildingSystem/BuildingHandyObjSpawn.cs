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
        int layerMask;

        private void Start()
        {
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
        }
        private void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);

            //MouseOver Essential item 확인
            if (Physics.Raycast(ray, out hit, 10000, layerMask))
            {
                if (hit.collider.GetComponent<BuildingItemObj>() != null)
                {
                    if (hit.collider.GetComponent<BuildingItemObj>().toolType == InItemType.BuildingItemObj_Essential)
                    {
                        curMouseOverEssentialObj = hit.collider.GetComponent<BuildingItemObj>();
                        Debug.Log("Mouse is over EssentialObj");
                    }
                    else if (hit.collider.GetComponent<BuildingItemObj>().toolType == InItemType.BuildingItemObj_Additional)
                    {

                        Debug.Log("Mouse is over AdditionalObj");
                    }

                }
                else
                {
                    curMouseOverEssentialObj = null;
                }
            }

            //건축모드처럼 간이건축 설치 method
            if (Input.GetMouseButtonDown(0))
            {
                print("MouseDown");
                if (curInteractHandyObj == null) return;

                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {

                    // print("Hit object" + hit.collider.name);

                    if (hit.collider.GetComponent<BuildingItemObj>() == null) //자재가 아닌걸 클릭 시
                    {
                        if (!curInteractHandyObj.ItemisSet && !curInteractHandyObj.IsFirstDrop)
                        {
                            if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Essential)
                            {
                                if (curInteractHandyObj.transform.position.y <= curInteractHandyObjGroundMaxYPos && curInteractHandyObj.transform.position.y >= curInteractHandyObjGroundMinYPos)
                                {
                                    print("ItemisSet = true 1 ");
                                    SetEssentialItemObj();
                                }
                            }
                            else if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Additional)
                            {
                                print("AdditionalItemisSet = true 2 ");
                                SetAdditionalItemObj();
                            }


                        }
                        else //처음 생성 시
                        {
                            print("ItemisSet = false 1");
                        }

                        return;
                    }

                    if (curInteractHandyObj.ItemisSet) //자재 클릭 + 세팅된 자재일 때
                    {
                        print("ItemisSet = false 2");
                        curInteractHandyObj = hit.collider.GetComponent<BuildingItemObj>();
                        curInteractHandyObj.ItemisSet = false;
                    }
                    else //자재 클릭 + 무빙중일 때
                    {
                        if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Essential)
                        {
                            if (curInteractHandyObj.transform.position.y <= curInteractHandyObjGroundMaxYPos && curInteractHandyObj.transform.position.y >= curInteractHandyObjGroundMinYPos)
                            {
                                print("EssentialItemisSet = true 2 ");
                                SetEssentialItemObj();

                            }
                        }
                        else if (curInteractHandyObj.toolType == InItemType.BuildingItemObj_Additional)
                        {
                            print("AdditionalItemisSet = true 2 ");
                            SetAdditionalItemObj();
                        }


                    }
                }
                else
                {
                    print("ItemisSet = true 3 ");

                }

            }

        }


        private void SetEssentialItemObj()
        {
            curInteractHandyObj.ItemisSet = true;
            curInteractHandyObj.IsFirstDrop = false;
        }

        private void SetAdditionalItemObj()
        {
            if (curMouseOverEssentialObj != null)
            {
                curInteractHandyObj.ItemisSet = true;
                curInteractHandyObj.IsFirstDrop = false;
                curInteractHandyObj.gameObject.GetComponent<Billboard>().enabled = false;
                curInteractHandyObj.transform.parent = curMouseOverEssentialObj.transform;
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

