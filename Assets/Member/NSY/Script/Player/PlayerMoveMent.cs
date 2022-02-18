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






        MapTravel MapTravel;
        CameraManager CamManager;
        private void Start()
        {
            MapTravel = FindObjectOfType<MapTravel>();
            CamManager = FindObjectOfType<CameraManager>();

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
            if (Input.GetKey(KeyCode.Z))
            {
                TravelToArea(1);
            }
        }
        // public GameObject TransZero;
        // void Tele()
        // {
        //    transform.position = new Vector3(TransZero.transform.position.x , TransZero.transform.position.y , TransZero.transform.position.z);
        // }

        void TravelToArea(int AreaNum)
        {
            Vector3 newPos = MapTravel.AreaList[AreaNum].transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }

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


