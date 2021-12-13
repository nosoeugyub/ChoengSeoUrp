using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Cam
{
    public class CamreaCtrl : MonoBehaviour
    {
        [SerializeField] private float MouseSensitivity;
        private Transform parent;
        [HideInInspector]
        public bool CanRotate;
        // Start is called before the first frame update
        void Start()
        {
            parent = transform.parent;
            Cursor.lockState = CursorLockMode.Locked;
            CanRotate = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanRotate)
            {
                Rotate();
            }

        }


        private void Rotate()
        {
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            parent.Rotate(Vector3.up, MouseX);
        }
    }

}