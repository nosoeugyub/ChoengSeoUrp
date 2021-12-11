using Game.Move;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UIDistance : MonoBehaviour
    {
        public GameObject Player;
        public GameObject HiddisUi;
        private TextMesh HiddisTex;
        private float Dis;

        public void Start()
        {
            HiddisTex = GameObject.Find("Hiddis").GetComponent<TextMesh>();
        }
        public void Update()
        {
            ChechDis();
        }

        public void ChechDis()
        {
            GetComponent<Moving>().Distance(Dis,Player,HiddisUi);
         
        }
    }
} 


