using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NSY.Bug
{
    public class BugMoving : MonoBehaviour
    {
        [SerializeField] [Header("이동거리")] [Range(0, 50f)] float dist = 1f;
        [SerializeField] [Header("이동속도")] [Range(0, 50f)] float speed = 1f;
        [SerializeField] [Header("이동빈도")] [Range(0, 50f)] float treuquency = 1f;
        [SerializeField] [Header("이동높이")] [Range(0, 50f)] float WaveHight = 1f;

        Vector3 pos, localScale;
        bool dirRight = true;

        private void Start()
        {
            pos = transform.position;
            localScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (transform.position.x > dist)
            {
                dirRight = false;
            }
            else if(transform.position.x <-dist)
            {
                dirRight = true;
            }

            if (dirRight)
            {
                GoRighit();
            }
            else
                GoLeft();
        }

        void GoRighit()
        {
            localScale.x = 1;
            transform.transform.localScale = localScale;
            pos += transform.right * Time.deltaTime * speed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * treuquency) * WaveHight;
        }

        void GoLeft()
        {
            localScale.x = -1;
            transform.transform.localScale = localScale;
            pos -= transform.right * Time.deltaTime * speed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * treuquency) * WaveHight;
        }

    }
}


