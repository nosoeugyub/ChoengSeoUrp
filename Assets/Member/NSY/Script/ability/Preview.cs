using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    //총돌한 오브젝 콜라이더를 저장
    private List<Collider> colliderList = new List<Collider>();
    [SerializeField]
    private int layerGround; // 지상레어
    private const int IGNORE_RAYCAST_LAYER = 2;
    [Header("머테리얼")]
    [SerializeField] private Material green;
    [SerializeField] private Material red;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 충돌한 콜라이더 저장 및 관리
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
             colliderList.Add(other);
        }
       

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Remove(other);
        }
    }
}
