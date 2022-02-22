using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPosition : MonoBehaviour
{
    [SerializeField] public Transform Playertransform;
    //초반 튜토리얼 구간
    //표지판
    [SerializeField] public Transform FristPost;
    public GameObject PopUpPost;
    //사과나무
    [SerializeField] public Transform FristApple;
    public GameObject PopUpApple;
    //인벤토리 열기
    //미니맵 퀘스트
    [SerializeField] public Transform FirstMiniMap;
    public GameObject MiniMap;


    

    [SerializeField] Vector3 Uioffset;
    [SerializeField] Vector3 PlayTransUioffset;
    
    //public GameObject PopUpTuto001;

    // Update is called once per frame
    void Update()
    {
        PopUpPost.transform.position = Camera.main.WorldToScreenPoint(FristPost.position + Uioffset);
        PopUpApple.transform.position = Camera.main.WorldToScreenPoint(FristApple.position + Uioffset);
        MiniMap.transform.position = Camera.main.WorldToScreenPoint(Playertransform.position + Uioffset);

    }
}
