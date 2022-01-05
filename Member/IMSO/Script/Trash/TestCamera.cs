using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform ObjectToFollow;   //따라가야 할 오브젝트의 정보
    public float FollowSpeed = 10f;    //따라갈 스피드
    public float sensitivity = 100f;   //마우스 감도
    public float clamAngle = 70f;      //카메라 제한 각도;

    //마우스 인풋을 받을 변수
    private float rotX;
    private float rotY;

 
    public Transform realCamera;   //카메라의 정보
    public Vector3 dirNormalized;  //카메라의 방향
    public Vector3 finalDir;       //최종 방향
    public float minDistance;      //최소거리
    public float maxDistance;      //최대거리
    public float finalDistance;    //최종거리
    public float smoothness = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        //커서 없앰
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clamAngle, clamAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, ObjectToFollow.position, FollowSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        // Lerp = 두 점 사이를 부드럽게
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
