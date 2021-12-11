using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.State;
using Game.Move;

namespace Player.Movement
{
    public class PlayerMoveMent : PlyerState
    {

       // [SerializeField]
      //  private imso.StatusController theStatusController;

   //     [SerializeField]
        private Game.Move.Moving theMovingController;

        protected override void Start()
        {
         //   theStatusController = FindObjectOfType<imso.StatusController>();
            theMovingController = FindObjectOfType<Game.Move.Moving>();
        }

        protected override void Update()
        {
            base.Update();
            Move();
            Idle();
            
        }
        private void Idle() //정지및 점프 감지
        {
            if (!isJumping)
            {
                Startvec = IdleVec;
                Gravity = Physics.gravity.y;
                Weight = Gravity * Mess;
                Startvec.y += Weight * Time.deltaTime;
                if (theMovingController.CharControl.isGrounded)
                {
                    JumpCount = 0;
                    Startvec.y = 0.0f;
                }
                if (isjump() && JumpCount < MaxJumpCount)
                {
                    ++JumpCount;
                    Startvec.y = jumpForce;
                }
                IdleVec = Startvec;
            }
        }

        protected  bool isjump()
        {
            if (Input.GetButtonDown("Jump"))
            {
           //     theStatusController.DecreaseStamina(100);
                return true;
            }
            return false;
        }
        /// move
        public void Move()
        {
            VecDir = MovementTo();
            VecRD = (VecDir + IdleVec) * Time.deltaTime;

            GetComponent<Moving>().MoveTo(VecRD);

        }

        protected  Vector3 MovementTo()
        {
            Vector3 move = Vector3.zero;
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            move = (transform.forward * vertical + transform.right * horizontal).normalized;
            SpeedVec = move * moveSpeed;

            
            return SpeedVec;
        }


        
      






    }



}


