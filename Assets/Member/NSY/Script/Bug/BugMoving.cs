using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NSY.Bug
{
    public class BugMoving : MonoBehaviour
    {
        public float X, Y , height;

        public Vector3 tempPos;
        private void Start()
        {
            tempPos = transform.position;
        }

        private void FixedUpdate()
        {
            tempPos.x += X;
            tempPos.y = Mathf.Sin(Time.realtimeSinceStartup * Y) * height;
            transform.position = tempPos;
        }
    }
}


