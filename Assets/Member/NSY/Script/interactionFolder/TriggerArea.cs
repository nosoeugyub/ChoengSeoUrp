using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;
public class TriggerArea : MonoBehaviour
{
  
    int mainid;
    bool mainisId;


    int singid;
    bool singisid;
   
    bool isSign;

    public GameObject TalkMessage;
    public GameObject QuestBox;
    private void Start()
    {

        MainNpc mainNpc = new MainNpc();
        mainid = mainNpc.MainNPCID;
        mainisId = mainNpc.MainNPCIsid;


        sign SignNpc = new sign();
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
            //Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("팻말 퀘스트 시작해");
                Manager.Instance.OnPanel();
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
    public void Delay()
    {
        QuestBox.SetActive(true);
    }

}
