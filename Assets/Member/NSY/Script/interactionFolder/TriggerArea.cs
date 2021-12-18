using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;
public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    private MainNpc mainNpc;
    int mainid;
    bool mainisId;

    [SerializeField]
    private sign SignNpc;
    int singid;
    bool singisid;
   
    bool isSign;
    private void Start()
    {
        mainid = mainNpc.MainNPCID;
        mainisId = mainNpc.MainNPCIsid;

        singid = SignNpc.SignID;
        singisid = SignNpc.SignIsid;

    }
    private void Update()
    {
        isSign = Input.GetKeyDown(KeyCode.G);
    }


     void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("signNPC"))//퀘스트
        {
            Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("팻말 퀘스트 시작해");
                Manager.Instance.OnFirstQuest(singid, singisid);
            }
            else
                isSign = false;

        }

        if (col.gameObject.CompareTag("MainNPC"))//퀘스트
        {
            Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("메인 퀘스트 시작해");
                Manager.Instance.OnFirstQuest(mainid, mainisId);
            }
            else
                isSign = false;

        }

     



        // if (collsion.gameObject.CompareTag("Item"))//item
        //  {
        //      Debug.Log("ItemGEt");
        //     FindObjectOfType<InventoryManager>().AddItem(collsion.gameObject.GetComponent<ItemObject>().item, 1);

        //        }
    }
}
