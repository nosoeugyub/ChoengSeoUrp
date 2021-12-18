using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;
public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    private sign NPCsign;


    int id;
    bool isId;
    bool isSign;
    private void Start()
    {
        id = NPCsign.SignID;
        isId = NPCsign.SignIsid;
       
    }
    private void Update()
    {
        isSign = Input.GetKeyDown(KeyCode.G);
    }


     void OnTriggerStay(Collider col)
    {


        if (col.gameObject.CompareTag("Player"))//퀘스트
        {
            Debug.Log("충돌함");
            if (isSign)
            {
                Debug.Log("팻말 퀘스트 시작해");
                Manager.Instance.OnFirstQuest(id, isId);
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
