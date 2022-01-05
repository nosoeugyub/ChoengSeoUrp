//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SJ_CharacterController : MonoBehaviour
//{
//    [SerializeField] private Transform characterBody;
//    [SerializeField] private Transform cameraArm;
//    [SerializeField] private LayerMask layerMask;

//    public GameManager manager;
//    GameObject scanObject;
//    Vector3 dirVec;

//    float h;
//    float v;
//    bool jump;
//    public int speed = 15;
//    public int jumpPower = 15;
//    bool isJump;
//    Rigidbody rigid;
//    void Awake()
//    {
//        rigid = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        GetInput();
//        LookAround();
//        Move();
//        Jump();
//    }
//    void FixedUpdate()
//    {      
//        FreezeRotation();
//        RayCast();
//    }
//    void FreezeRotation()
//    {
//        rigid.angularVelocity = Vector3.zero;
//    }
//    void RayCast()
//    {
//        RaycastHit hit;
//        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);

//        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 5))
//        {
//            if (hit.collider != null)
//            {
//                //Debug.Log("이것은" + hit.transform.name);
//                scanObject = hit.collider.gameObject;
//            }
//            else
//            {
//                scanObject = null;
//            }                     
//        }
//    }
//    void GetInput()
//    {      
//        jump = manager.isAction ? false : Input.GetButtonDown("Jump");

//        if(Input.GetKeyDown(KeyCode.R) && scanObject != null)
//        {
//         //  manager.QAction(scanObject);
//        }
//    }

//    private void Move()
//    {
//        h = manager.isAction ? 0 : Input.GetAxis("Horizontal");
//        v = manager.isAction ? 0 : Input.GetAxis("Vertical");

//        Vector2 moveInput = new Vector2(h, v);
//        bool isMove = moveInput.magnitude != 0;

//        if(isMove)
//        {
//            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
//            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
//            Vector3 moveDir = lookForward * moveInput.y + lookRight *moveInput.x;

//            characterBody.forward = lookForward;
//            transform.position += moveDir * speed * Time.deltaTime;
//        }
//        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x,0f,cameraArm.forward.z).normalized, Color.red);
//    }
//    void Jump()
//    {
//        if (jump && !isJump)
//        {
//            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
//            isJump = true;
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Floor")
//        {
//            isJump = false;
//        }
//    }

//    private void LookAround()
//    {
//        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
//        Vector3 camAngle = cameraArm.rotation.eulerAngles;
//        float x = camAngle.x - mouseDelta.y;

//        if(x < 180f)
//        {
//            x = Mathf.Clamp(x, -1f, 70f);
//        }
//        else
//        {
//            x = Mathf.Clamp(x, 335f, 361f);
//        }
//        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
//    }
//}
