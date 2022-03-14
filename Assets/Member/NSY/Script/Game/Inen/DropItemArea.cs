using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Inven;
using UnityEngine.EventSystems;
using System;

namespace NSY.Iven
{
    public class DropItemArea : MonoBehaviour, IDropHandler
    {
        public event Action OnDropEvent;


        public void OnDrop(PointerEventData eventData)
        {
            if (OnDropEvent != null)
            {
                OnDropEvent();
            }
        }
    }

}

