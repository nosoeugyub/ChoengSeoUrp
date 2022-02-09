using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Player;
using System;
using TT.ObjINTERACT;
namespace NSY.Manager
{
    public class TutorialManager : MonoBehaviour
    {
        //추가 컴포넌트
        [SerializeField]
        PlayerInput PlayerInput;
        [SerializeField]
        SignPost signpost;

        private int popUpIndex;
        private float waitTime = 2f;

       
         void Update()
        {
            TutorPopup();
        }

        void TutorPopup()
        {
            for (int i = 0; i < SuperManager.Instance.uimanager.TutorpopUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    SuperManager.Instance.uimanager.TutorpopUps[popUpIndex].SetActive(true);
                }
            }
            if (popUpIndex == 0)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                    || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    Debug.Log("눌렀으니 끄셈 ㅡㅡ");
                    ChangePopup();
                    popUpIndex++;
                }
            }
            else if(popUpIndex == 1)//표지판 가는 유아이
            {
                Debug.Log("2번째 ON...");
                signpost.FirstSign = true;
                popUpIndex++;



            }
        }
        private void ChangePopup()
        {
            for (int i = 0; i < SuperManager.Instance.uimanager.TutorpopUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    SuperManager.Instance.uimanager.TutorpopUps[popUpIndex].SetActive(false);
                   
                }

            }
        }

    }


}

