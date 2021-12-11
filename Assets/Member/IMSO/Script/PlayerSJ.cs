using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSJ : MonoBehaviour
{
    public GameManager manager;
    public float speed;

    Rigidbody rigid;
    float h;
    float v;
    bool isHorizonMove;

    GameObject scanObject;

    //현재 바라보고 있는 방향 값을 가진 변수
    Vector3 dirVec;
    Vector3 moveVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move Value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //Check Button &Up
        bool hDown = manager.isAction ? false : Input.GetButton("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButton("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButton("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButton("Vertical");

        //Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;

        //Direction
        if (vDown && v == 1)
            dirVec = Vector3.forward;
        else if (vDown && v == -1)
            dirVec = Vector3.back;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        if(Input.GetButtonDown("Jump")&&scanObject != null)
        {
            Debug.Log("This is : " + scanObject.name);
            manager.Action(scanObject);
        }
    }

    public void FixedUpdate()
    {
        //Move
        Vector3 moveVec = isHorizonMove ? new Vector3(h, 0, 0) : new Vector3(0, 0, v);
        rigid.velocity = new Vector3(0, 0, v).normalized;
        transform.position += moveVec * speed * Time.deltaTime;
        Debug.Log(dirVec);
        //Ray
        Debug.DrawRay(rigid.position, dirVec * 1f, new Color(0, 1, 0));

        /*RaycastHit[] rayHit = Physics.RaycastAll(rigid.position, dirVec, 1f, LayerMask.GetMask("Object"));

        if (rayHit[0].collider != null)
        {
            scanObject = rayHit[0].collider.gameObject;
        }
        else
            scanObject = null;
        */
    }
}
