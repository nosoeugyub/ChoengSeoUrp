using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Player.State
{
    public  class PlyerState : MonoBehaviour
    {
        [SerializeField]
        protected string PlayerClassName = "PlayerState";

        //[SerializeField] private Transform cameraArm;



        [Header("Smell")]     
        Collider[] CheckinHideZone;
         [SerializeField]
        private LayerMask Mask;
        [SerializeField]
        private bool isSmell = false;


       //  GameObject[] HideObject;
       // GameObject[] ChidHideObj;

       [Header("jump")]
        [SerializeField] protected bool isJumping;
        [SerializeField] protected float JumpCount;
        [SerializeField] protected float MaxJumpCount;
        [SerializeField] protected float jumpForce;

        [Header("Move")]
        public float moveSpeed;

        [Header("Anim")]
        public Animator Anim;
        
        [Header("Rigid")]
        protected Vector3 Startvec, VecDir, VecRD;
        protected Vector3 IdleVec = Vector3.zero; //가만히 있을때 상태
        protected float Gravity; //중력
        protected float Mess=1;//질량
        protected float Weight; //무게
        protected Vector3 SpeedVec;//속도

        protected virtual void Start()
        {
           
        }
        protected virtual void FixedUpdate()
        {
       //  if (OnSmell())
        // {
       //     return;
      //   }
           
        }
        protected virtual void Update()
        {
            //LookAround();
        }


        //소정씨 코드
        //private void LookAround()
        //{
        //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //    Vector3 camAngle = cameraArm.rotation.eulerAngles;
        //    float x = camAngle.x - mouseDelta.y;

        //    if (x < 180f)
        //    {
        //        x = Mathf.Clamp(x, -1f, 70f);
        //    }
        //    else
        //    {
        //        x = Mathf.Clamp(x, 335f, 361f);
        //    }
        //    cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        //}



        private bool OnSmell()
        {

            CheckinHideZone = Physics.OverlapSphere(transform.position, 10, Mask);
           
               
            foreach (Collider CheckinhideZone in CheckinHideZone)
            {

                if (Input.GetKey(KeyCode.E))
                {
                    CheckinhideZone.gameObject.transform.Find("Hiddis").gameObject.SetActive(true);
                    isSmell = true;
                    Debug.Log("발견함 ㅋㅋ");
                   
                    
                }
                else
                {
                    CheckinhideZone.gameObject.transform.Find("Hiddis").gameObject.SetActive(false);
                    isSmell = false;
                }
            }
            return true;

        }
      


    }

}
