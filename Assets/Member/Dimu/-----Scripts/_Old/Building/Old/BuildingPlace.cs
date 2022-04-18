using UnityEngine;
namespace DM.Building
{

    public class BuildingPlace : MonoBehaviour
    {
        public bool isBuildMode = false;
        GameObject instanceObj;
        //public GameObject buildingUI;
        //public GameObject buildingCancelUI;
        //public GameObject decideBuildingPopup;
        public void Update()
        {
            if (isBuildMode)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100000, 1 << 8))
                {
                    Debug.Log("땅 레이 쏘는중");
                    instanceObj.transform.position = hit.point;
                }
                else
                    return;

                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("클릭업");
                    OffBuildMode();
                }
            }
        }
        public void CancelBuild()
        {
            Destroy(instanceObj);
            OffBuildMode();
        }
        public void OffBuildMode()
        {
            isBuildMode = false;
            //buildingUI.SetActive(true);
            //decideBuildingPopup.SetActive(false);
            //buildingCancelUI.SetActive(false);

            BuildingObject instB= instanceObj.transform.GetChild(0).GetComponent<BuildingObject>();
            instB.StartBuild();
        }

        public void OnBuildMode(GameObject fab)
        {
            isBuildMode = true;
            instanceObj = Instantiate(fab) as GameObject;
            //buildingUI.SetActive(false);
            //buildingCancelUI.SetActive(true);
            //상태 UI 제외 모든 UI 끄기로 업그레이드 해야 함.
        }
    }
}