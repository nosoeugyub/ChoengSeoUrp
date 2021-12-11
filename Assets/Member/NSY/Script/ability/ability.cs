using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.State;

namespace Player.abiliyBudiing
{
    //건축목록 클래스
    [System.Serializable]
    public class craft
    {
        public string craftName;
        public GameObject go_Prefab;//실제 놓아지는 
        public GameObject go_PreviewPrefab;//미리보기
    }


    public   class ability : MonoBehaviour
    {
        //필요 메뉴 레이
        private RaycastHit hitInfo;
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private float range;
        Vector3 destion;

        [Header("카메라")]
        protected Camera camera;
        [Header("플레이어")]
        [SerializeField] protected Transform PlayerTf;

        [Header("집짓기 Ui")]
        private bool isActiveated = false;
        [SerializeField] protected GameObject go_BaseUi;
        [SerializeField] protected craft[] craft_Home;
        protected GameObject go_Preview;//미리보기 변수담을 변수
        protected bool isPreviewActivated = false; //미리보기 상태 변수
        protected GameObject go_Prefab;// 실제 소환
        protected void Awake()
        {
            camera = Camera.main;
        }

        protected  void Update()
        {
            BuildingButton();

        }
        //ui 구현부
        void BuildingButton()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            {
                OpenPanel();
            }
            if (isPreviewActivated)
            {
                PreviewPositionUpdate();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }
       
            if (Input.GetButtonDown("Fire1"))
            {
                Build();
            }

        }
       

        private void OpenPanel()
        {
            if (!isActiveated)
            {
                OpenBulidingPanel();
            }
            else
                CloseBulidingPanel();

        }
        private void Cancel()
        {
            if (isPreviewActivated)
            {
                Destroy(go_Preview);
            }
            isActiveated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
            go_BaseUi.SetActive(false);

        }    
        private void OpenBulidingPanel()
        {
            isActiveated = true;
            go_BaseUi.SetActive(true);
        }
        private void CloseBulidingPanel()
        {
            isActiveated = false;
            go_BaseUi.SetActive(false);
        }


        //클릭 슬롯
        public void SlotClick(int _slotNumber)
        {

            go_Preview = Instantiate(craft_Home[_slotNumber].go_PreviewPrefab, PlayerTf.transform.position, Quaternion.identity);
            go_Prefab = craft_Home[_slotNumber].go_Prefab;
            isPreviewActivated = true;
            go_BaseUi.SetActive(false);
        }
        //포지션
        public  void PreviewPositionUpdate()
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo, range, layerMask))
            {
                if (hitInfo.transform != null)
                {
                    Vector3 _locaton = hitInfo.point;
                    go_Preview.transform.position = _locaton;
                }
            }
        }
    
        //짓기
        protected  void Build()
        {
            if (isPreviewActivated)
            {
                Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
                Destroy(go_Preview);
                isPreviewActivated = false;
              
                go_Preview = null;
                go_Prefab = null;
            }

        }

    }
}



