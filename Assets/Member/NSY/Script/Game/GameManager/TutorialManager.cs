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
        //첫번째 튜토리얼 표지판보기
        [SerializeField]
        SignPost signpost;
        [SerializeField]
        GameObject signpostObj;


        [SerializeField]
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
                    SuperManager.Instance.uimanager.TutorpopUps[i].SetActive(true);
                   
                }
                else
                {
                  
                    SuperManager.Instance.uimanager.TutorpopUps[i].SetActive(false);
                }
                    

                
            }
            if (popUpIndex == 0)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                    || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    StartCoroutine(ZeroTuto());
                }
                StopCoroutine(ZeroTuto());
            }
            else if(popUpIndex == 1)//표지판 가는 유아이
            {
     
                //StartCoroutine(OneTuto());

            }
        }
        IEnumerator ZeroTuto()
        {
            popUpIndex = -1;
            yield return new WaitForSeconds(1f);
            signpostObj.GetComponent<SignPost>().enabled = true;
            yield return new WaitForSeconds(2.7f);
            popUpIndex =1;
            yield return new WaitForSeconds(3f);
            popUpIndex = -1;
        }


        IEnumerator OneTuto()
        {
           
             yield return new WaitForSeconds(5f);
            popUpIndex++;
        }
        IEnumerator SecondTuto()//이벤토리 열라는 튜토리얼 
        {
            yield return new WaitForSeconds(0.3f);
        }
        IEnumerator ThirdTuto()//지도  열라는 튜토리얼 
        {
            yield return new WaitForSeconds(0.3f);
        }
    }


}

