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

      //  public GameManager manager;
       // GameObject scanObject;

      

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
      //  protected override void  FixedUpdate()
      //  {
          //  FreezeRotation();
          //  RayCast();
       // }
        //소정씨
     //   void FreezeRotation()
      //  {
      //      rigid.angularVelocity = Vector3.zero;
      //  }
       // void RayCast()
       // {
       //     RaycastHit hit;
        //    Debug.DrawRay(transform.position, transform.forward * 5, Color.red);

        //    if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 5))
      //      {
        //        if (hit.collider != null)
        //        {
         //           Debug.Log("이것은" + hit.transform.name);
        //            scanObject = hit.collider.gameObject;
        //        }
        //        else
        //        {
         //           scanObject = null;
          //      }
         //   }
      //  }
        /// <summary>
        /// 
        /// </summary>
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

          //  if (Input.GetKeyDown(KeyCode.R) && scanObject != null) // 소정씨 코드
          //  {
           //     manager.Action(scanObject);
           // }
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


