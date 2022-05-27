using Game.Cam;
using NSY.Manager;
using TT.MapTravel;
using UnityEngine;
namespace NSY.Player
{
    public class PlayerMoveMent : MonoBehaviour
    {
        //플립
        [SerializeField]
        Transform meshrender;

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
        }

        public void FixedUpdate()
        {
            if (!CamManager.IsZoom && !playerController.playerinteract.IsAnimating())
            {
                Move();
                Flip();
            }
            idle();
            //////Zess's code//////
            //if (Maptravel)
            //{
            //    CharacterTravel();
            //}
            //////End of "Zess's code"//////
        }

        protected void Move()
        {
            Vector2 MoveDelta = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            isMove = MoveDelta.magnitude != 0; // 0이면 이동입력이 없는것 
            playerController.SpritePlayerAnim.SetBool("isWalk", isMove);


            lookForward = new Vector3(playerController.maincamera.transform.forward.x, 0f, playerController.maincamera.transform.forward.z).normalized;//보는 방향을 바라보는 방향 카메라

            if (isMove)
            {
                SuperManager.Instance.soundManager.PlaySFX("FootstepGrass01");

                LookRight = new Vector3(playerController.maincamera.transform.right.x, 0f, playerController.maincamera.transform.right.z).normalized; //보는방향을 평면화
                MoveVec = (lookForward * MoveDelta.y + LookRight * MoveDelta.x).normalized;
                MoveVec *= playerController.PlayerSpeed;

                Vector3 CurVec = MoveVec;
                Vector3 movement = (CurVec + idleMove) * Time.deltaTime;
                playerController.characterCtrl.Move(movement);
                //CurVec에 MapTravel 백터를 수정하시면 됩니다.

            }
            else
                playerController.characterCtrl.Move(idleMove);

            transform.forward = lookForward;
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
            if (FlipMove > 0)
            {
                meshrender.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (FlipMove < 0)
            {
                meshrender.localEulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
}


////////Zess's code//////
//void CharacterTravel()
//{
//    Maptravel = false;
//    switch (curAreaNum)
//    {
//        case 0:
//            TravelToOuterArea(1);
//            break;
//        case 1:
//            TravelToInnerArea(0);
//            break;
//        case 2:
//            TravelToInnerArea(1);
//            break;
//        case 3:
//            TravelToInnerArea(2);
//            break;
//        case 4:
//            TravelToInnerArea(3);
//            break;
//        case 5:
//            TravelToInnerArea(4);
//            break;
//        case 6:
//            TravelToInnerArea(5);
//            break;
//        case 7:
//            TravelToInnerArea(6);
//            break;
//        case 8:
//            TravelToInnerArea(7);
//            break;
//        case 9:
//            TravelToInnerArea(8);
//            break;
//        case 10:
//            TravelToInnerArea(9);
//            break;
//        case 11:
//            TravelToInnerArea(10);
//            break;
//        case 12:
//            TravelToInnerArea(11);
//            break;

//    }
//}
//void TravelToOuterArea(int AreaNum)
//{
//    Vector3 newPos = MapTravel.OuterAreaList[AreaNum].transform.position;
//    newPos.y = transform.position.y;
//    transform.position = newPos;
//}

//void TravelToInnerArea(int AreaNum)
//{
//    Vector3 newPos = MapTravel.InnerAreaList[AreaNum].transform.position;
//    newPos.y = transform.position.y;
//    transform.position = newPos;
//}
////////End of "Zess's code"//////