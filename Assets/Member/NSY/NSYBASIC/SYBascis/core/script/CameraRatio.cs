using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JMBasic
{

    public class CameraRatio : MonoBehaviour
    {
        //float targetWidthAspect = 18.0f;
        //float targetHeightAspect = 9.0f;

        //float targetWidthAspect = 4.0f;
        //float targetHeightAspect = 2.0f;
        public float DefaultRatio = 3.2f;
        Camera mainCamera;
        // Start is called before the first frame update
        void Awake()
        {
            //DefaultRatio = GlobalData.Instance.SetIngameCameraSize(DefaultRatio);

            mainCamera = gameObject.GetComponent<Camera>();

            mainCamera.orthographicSize = ((float)Screen.height / ((float)Screen.width / 18.0f)) / 9.0f;
            mainCamera.orthographicSize *= DefaultRatio;// 3.2f;// 2.85f;//3.2f;
                                                        //3.2 = 1280
                                                        //2.85 = 1136

            /*
            mainCamera.orthographicSize = ((float)Screen.height / 2.0f) / 100.0f;

            while(mainCamera.orthographicSize > 3.84f)
            {
                mainCamera.orthographicSize /= 2.0f;
            }
            */
            /*
            targetHeightAspect = ((float)Screen.height / (float)Screen.width) * 4.0f;

            mainCamera = gameObject.GetComponent<Camera>();

            mainCamera.aspect = targetWidthAspect / targetHeightAspect;

            float widthRatio = (float)Screen.width / targetWidthAspect;
            float heightRatio = (float)Screen.height / targetHeightAspect;

            float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
            float widthadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

            if (heightRatio > widthRatio)
                widthadd = 0.0f;
            else
                heightadd = 0.0f;

            mainCamera.rect = new Rect(
                mainCamera.rect.x + Mathf.Abs(widthadd),
                mainCamera.rect.y + Mathf.Abs(heightadd),
                mainCamera.rect.width + (widthadd * 2),
                mainCamera.rect.height + (heightadd * 2));
                */
        }
    }
}
