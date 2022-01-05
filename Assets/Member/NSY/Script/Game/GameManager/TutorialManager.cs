using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Player;
using System;

namespace NSY.Manager
{
    public class TutorialManager : MonoBehaviour
    {
        //추가 컴포넌트
        [SerializeField]
        PlayerInput PlayerInput;


        private int popUpIndex;
        private float waitTime = 2f;

         void Start()//이벤트 할당
        {
           EventManager.HitFoodBox += TutorPopup;
            EventManager.UnHitFoodBox += ChangePopup;
        }

      
        private void OnDisable()//해제
        {
            EventManager.HitFoodBox -= TutorPopup;
            EventManager.UnHitFoodBox -= ChangePopup;
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

