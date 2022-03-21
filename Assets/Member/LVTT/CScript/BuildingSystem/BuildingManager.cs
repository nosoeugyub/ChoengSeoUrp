using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TT.BuildSystem
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField]public  Transform CurBuilding;
        [HideInInspector]
        public int BuildItemDragIndex = 0;
        [HideInInspector]
        public bool OnBuildItemDrag = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
      
        }

      
    }
}

