using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cam
{
    public enum MapArea { North, South, East, West }
    public class CameraRotate : MonoBehaviour
    {
        public MapArea Area;
        CameraManager CamManager;
        void Start()
        {
            CamManager = FindObjectOfType<CameraManager>();
        }

        // Update is called once per frame
        void Update()
        {
           

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Debug.Log("TouchWestArea");
                CamManager.ChangeCamRotToArea(this.Area);
                

            }
        }
    }
}

