using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Iven
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] StatDisPlay[] statsdisplay; //속성 배열 
        [SerializeField] string[] statName; //이름 배열

        private PlayerController Player;

        private void OnValidate()
        {
            statsdisplay = GetComponentsInChildren<StatDisPlay>();

        }



    }


}
