using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public class CameraIntro : MonoBehaviour
    {
        [SerializeField]CameraManager CamManager;
        void Start()
        {
            StartCoroutine(IntroRoutine());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator IntroRoutine()
        {
            yield return new WaitForSeconds(1);
            CamManager.MainCamera.SetActive(false);
          //  CamManager.DeactiveSubCamera(0);
            CamManager.ActiveSubCamera(1);      
            yield return new WaitForSeconds(0.5f);//stayTime = 메안 뷰 -> Object 뷰 바꾸는 시간  +뷰 유지 시간
            CamManager.DeactiveSubCamera(1);
            CamManager.ActiveSubCamera(2);
        }
    }

}
