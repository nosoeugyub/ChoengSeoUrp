using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;
using DM.Inven;
using Game.NPC;
public class TriggerArea : MonoBehaviour
{
    //촌장
    [SerializeField]
    private MainNpc mainNpc;
    int mainid;
    bool mainisId;
    //팻말
    [SerializeField]
    private sign SignNpc;
    int singid;
    bool singisid;
   /// <summary>
   /// 감지
   /// </summary>
    bool isSign;
    bool isTriggering;
    GameObject TriggerNPC;


    public GameObject TalkMessage;
    public GameObject QuestBox;
    private void Start()
    {
        mainid = mainNpc.MainNPCID;
        mainisId = mainNpc.MainNPCIsid;

        singid = SignNpc.SignID;
        singisid = SignNpc.SignIsid;

    }
    private void Update()
    {
        isSign = Input.GetKeyDown(KeyCode.R);
    }


     void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("signNPC"))//퀘스트
        {
            isTriggering = true;
            TriggerNPC = col.gameObject;
            //Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("팻말 퀘스트 시작해");
              
            }
        }

        if (col.gameObject.CompareTag("MainNPC"))//퀘스트
        {
            /*Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("메인 퀘스트 시작해");
                Manager.Instance.OnFirstQuest(mainid, mainisId);
            }
            else
                isSign = false;
            */
            if(Input.GetKeyDown(KeyCode.R))
            {
                TalkMessage.SetActive(true);

                Invoke("Delay", 6f);                           
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("signNPC"))
        {
            isTriggering = false;
            TriggerNPC = null;
        }
    }
    public void Delay()
    {
        QuestBox.SetActive(true);
    }

}
