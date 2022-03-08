using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.Test
{
    public class UIOnOff : MonoBehaviour
    {
        [SerializeField] GameObject[] UIList;

        void Start()
        {

        }
        void Update()
        {

        }

        public void TurnOnUI(int UINum)
        {
            UIList[UINum].SetActive(true);
        }

        public void TurnOffUI(int UINum)
        {
            UIList[UINum].SetActive(false);
        }
    }
}

