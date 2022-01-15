using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LVTT.Test
{
    public class ThirdPersonMovement : MonoBehaviour
    {
    CharacterController controller;

        public float speed=6f;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.normalized.magnitude>=0.1f)
            {
                controller.Move(direction * speed * Time.deltaTime);
            }
        }
    }

}
