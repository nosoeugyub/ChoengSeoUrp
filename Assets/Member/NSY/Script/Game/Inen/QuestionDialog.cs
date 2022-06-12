using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using System;
using UnityEngine.UI;
namespace NSY.Iven
{
    public class QuestionDialog : MonoBehaviour
    {
     
       
        public event Action OnYesEvent;
        public event Action OnNoEvent;
       public event Action<BaseItemSlot> OnLeftClickEvent;
        public void Show()
        {
            gameObject.SetActive(true);// 유아이 활성화
          
            OnYesEvent = null;
            OnNoEvent = null;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
       
        public void OnYesButtonClick()
        {
            if (OnYesEvent != null)
            {
                OnYesEvent();
            }
            Hide();
        }
        public void OnNoButtonClick()
        {
            if (OnNoEvent != null)
            {
                OnNoEvent();
            }
            Hide();
        }

    }

}

