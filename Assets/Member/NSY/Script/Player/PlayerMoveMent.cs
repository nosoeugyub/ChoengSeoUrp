using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.State;
using Game.Move;

namespace Player.Movement
{
    public class PlayerMoveMent : PlyerState
    {

        public float camrotSpeed;



        private Game.Move.Moving theMovingController;

        [SerializeField] private Transform cameraArm;



        protected override void Start()
        {
         //   theStatusController = FindObjectOfType<imso.StatusController>();
            theMovingController = FindObjectOfType<Game.Move.Moving>();
        }

        protected override void FixedUpdate()
        {
            base.Update();
            Move();
            Idle();
           //LookAround();


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
            if (Input.GetKeyDown(KeyCode.LeftAlt))
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
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            if (isMove)
            {
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                move = lookForward * moveInput.y + lookRight * moveInput.x;


                transform.forward = lookForward;
                move *=  moveSpeed;

              
            }
            return move;


        }


        private void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 camAngle = cameraArm.rotation.eulerAngles;

            cameraArm.rotation = Quaternion.Euler(camAngle.x, camAngle.y + mouseDelta.x* camrotSpeed, camAngle.z);
        }
    }
}


