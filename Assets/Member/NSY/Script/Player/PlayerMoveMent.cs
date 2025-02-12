﻿using NSY.Manager;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerMoveMent : MonoBehaviour//, IDataManager
    {
        //플립
        [SerializeField]
        Transform meshrender;

        [SerializeField]
        PlayerController playerController;
        float Mass = 30;

        internal Vector3 idleMove = Vector3.zero;
        internal Vector3 lookForward;
        internal Vector3 LookRight;
        public Vector3 MoveVec;
        public Vector3 movement;

        internal bool isMove;
        internal bool canMove;
        internal int canMoveCount;
        //////Zess's code//////
        [HideInInspector]
        public int curAreaNum;
        [HideInInspector]
        public bool Maptravel;


        //////End of "Zess's code"//////
        private void Awake()
        {
            Maptravel = false;
            curAreaNum = 1;

            canMove = true;
            canMoveCount = 0;
            InitForward();
        }
        public void SetIsMove(bool ismove)
        {
            if (ismove)
            {
                canMoveCount--;

                if (canMoveCount <= 0)
                    canMove = ismove;
            }
            else
            {
                canMoveCount++;
                canMove = ismove;
                playerController.SpritePlayerAnim.SetBool("isWalk", false);

            }
        }
        public void FixedUpdate()
        {
            if (canMove)
            {
                Move();
                Flip();
            }
            InitForward();
            idle();
        }
        public void InitForward()
        {
            lookForward = new Vector3(playerController.maincamera.transform.forward.x, 0f, playerController.maincamera.transform.forward.z).normalized;//보는 방향을 바라보는 방향 카메라
            transform.forward = lookForward;

        }
        protected void Move()
        {
            Vector2 MoveDelta = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            isMove = MoveDelta.magnitude != 0; // 0이면 이동입력이 없는것 
            playerController.SpritePlayerAnim.SetBool("isWalk", isMove);

            //lookForward = new Vector3(playerController.maincamera.transform.forward.x, 0f, playerController.maincamera.transform.forward.z).normalized;//보는 방향을 바라보는 방향 카메라

            if (isMove)
            {
                SuperManager.Instance.soundManager.PlaySFX("FootstepGrass01");
                LookRight = new Vector3(playerController.maincamera.transform.right.x, 0f, playerController.maincamera.transform.right.z).normalized; //보는방향을 평면화
                MoveVec = (lookForward * MoveDelta.y + LookRight * MoveDelta.x).normalized;
                MoveVec *= playerController.PlayerSpeed;
            
                Vector3 CurVec = MoveVec;
                movement = (CurVec + idleMove) * Time.deltaTime;
            
                playerController.characterCtrl.Move(movement);
            }
            else
                playerController.characterCtrl.Move(idleMove);

           // transform.forward = lookForward;
        }

        public void idle()
        {
            Vector3 move = idleMove;
            float Gravity = Physics.gravity.y;
            move.y += Gravity * Mass * Time.deltaTime;

            playerController.characterCtrl.Move(move * Time.deltaTime);
        }

        public void Flip()
        {
            float FlipMove = Input.GetAxisRaw("Horizontal");
            bool facingRight = true;
            facingRight = !facingRight;
            FlipMoveing(FlipMove);
        }

        private void FlipMoveing(float FlipMove)
        {
            if (FlipMove > 0)
            {
                ChangeYAngles(0);
            }
            else if (FlipMove < 0)
            {
                ChangeYAngles(180);
            }
        }

        private void ChangeYAngles(float yAngle)
        {
            meshrender.localEulerAngles = new Vector3(0, yAngle, 0);
        }

        public void MoveTowardsTarget(Vector3 target)
        {
            Vector3 offset = target - transform.position;
            Vector3 top = Vector3.up;
            playerController.characterCtrl.Move(top * 15);
            playerController.characterCtrl.Move(offset);
            playerController.characterCtrl.Move(-top * 14);


        }
        public void MoveTowardsTarget(Vector3 target, bool isRight)
        {
            Vector3 offset = target - transform.position;
            Vector3 top = Vector3.up;
            playerController.characterCtrl.Move(top * 15);
            playerController.characterCtrl.Move(offset);
            playerController.characterCtrl.Move(-top * 14);

            if (isRight)
            {
                ChangeYAngles(0);
            }
            else
            {
                ChangeYAngles(180);

            }
        }

        public void LoadData()
        {
            this.movement = ES3.Load<Vector3>("playerpos");
        }
        public void SaveData()
        {
            ES3.Save("playerpos", this.movement);
        }
        //public void LoadData(SaveData data)
        //{

        //}

        //public void SaveData(ref SaveData data)
        //{
        //    this.movement = data.PlayerVector;
        //}
    }
}


