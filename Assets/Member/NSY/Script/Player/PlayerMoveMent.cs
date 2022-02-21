using UnityEngine;
using Game.Cam;
using TT.MapTravel;
namespace NSY.Player
{
    public class PlayerMoveMent : MonoBehaviour
    {
        //플립
        [SerializeField]
        SpriteRenderer spriterender;

        [SerializeField]
        PlayerController playerController;
        float Mass = 10;

        internal Vector3 idleMove = Vector3.zero;
        internal Vector3 lookForward;
        internal Vector3 LookRight;
        internal Vector3 MoveVec;

        internal bool isMove;
        //////Zess's code//////
        [HideInInspector]
        public int curAreaNum;
        [HideInInspector]
        public bool Maptravel;
        MapTravel MapTravel;
        CameraManager CamManager;
        //////End of "Zess's code"//////
        private void Start()
        {
            //////Zess's code//////
            MapTravel = FindObjectOfType<MapTravel>();
            CamManager = FindObjectOfType<CameraManager>();
            Maptravel = false;
            curAreaNum = 1;
            //////End of "Zess's code"//////
            
            // spriterender = GetComponent<SpriteRenderer>();
        }


        public void FixedUpdate()
        {

            if (!CamManager.IsZoom)
            {
                Move();
               Flip();
            }

            idle();

            //////Zess's code//////
            if (Maptravel)
            {
                CharacterTravel();
            }
            //////End of "Zess's code"//////

            

        }

        //////Zess's code//////
        void CharacterTravel()
        {
            Maptravel = false;
            switch (curAreaNum)
            {
                case 0:
                    TravelToOuterArea(0);
                    break;
                case 1:
                    TravelToOuterArea(1);
                    break;
                case 2:
                    TravelToOuterArea(2);
                    break;
                case 3:
                    TravelToOuterArea(3);
                    break;
                case 4:
                    TravelToOuterArea(4);
                    break;
                case 5:
                    TravelToOuterArea(5);
                    break;
                case 6:
                    TravelToOuterArea(6);
                    break;
                case 7:
                    TravelToOuterArea(7);
                    break;
                case 8:
                    TravelToInnerArea(0);
                    break;
                case 9:
                    TravelToInnerArea(1);
                    break;
                case 10:
                    TravelToInnerArea(2);
                    break;
                case 11:
                    TravelToInnerArea(3);
                    break;
                case 12:
                    TravelToInnerArea(4);
                    break;
                case 13:
                    TravelToInnerArea(5);
                    break;
                case 14:
                    TravelToInnerArea(6);
                    break;
                case 15:
                    TravelToInnerArea(7);
                    break;
                case 16:
                    TravelToInnerArea(8);
                    break;
                case 17:
                    TravelToInnerArea(9);
                    break;
                case 18:
                    TravelToInnerArea(10);
                    break;
            }
        }
            void TravelToOuterArea(int AreaNum)
        {
            Vector3 newPos = MapTravel.OuterAreaList[AreaNum].transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }

        void TravelToInnerArea(int AreaNum)
        {
            Vector3 newPos = MapTravel.InnerAreaList[AreaNum].transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
        //////End of "Zess's code"//////

        protected void Move()
        {
            Vector2 MoveDelta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            isMove = MoveDelta.magnitude != 0; // 0이면 이동입력이 없는것 
            playerController.SpritePlayerAnim.SetBool("isWalk", isMove);
            if (isMove)
            {

                lookForward = new Vector3(playerController.maincamera.transform.forward.x, 0f, playerController.maincamera.transform.forward.z).normalized;//보는 방향을 바라보는 방향 카메라
                LookRight = new Vector3(playerController.maincamera.transform.right.x, 0f, playerController.maincamera.transform.right.z).normalized; //보는방향을 평면화
                MoveVec = (lookForward * MoveDelta.y + LookRight * MoveDelta.x).normalized;
                MoveVec *= playerController.PlayerSpeed;

                transform.forward = lookForward;
                Vector3 CurVec = MoveVec;
                Vector3 movement = (CurVec + idleMove) * Time.deltaTime;
               playerController.characterCtrl.Move(movement);
                //CurVec에 MapTravel 백터를 수정하시면 됩니다.
             
            }
         
            else
                playerController.characterCtrl.Move(idleMove);





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
            if (FlipMove>0)
            {
                spriterender.flipX = false;
            }
            else if(FlipMove< 0)
            {
                spriterender.flipX = true;
            }
           
        }

    }
}


