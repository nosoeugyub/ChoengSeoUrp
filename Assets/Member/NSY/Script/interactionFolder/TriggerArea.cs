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
    private void Start()
    {
        id = NPCsign.SignID;
        isId = NPCsign.SignIsid;
    }
    private void Update()
    {
     
    }


    private void OnTriggerEnter(Collider collsion)
    {

        
        if (collsion.gameObject.CompareTag("Player"))//퀘스트
        {
            Debug.Log("충돌함");
           

                Debug.Log("팻말 퀘스트 시작해");
                Manager.Instance.OnFirstQuest(id, isId);
        }
       // if (collsion.gameObject.CompareTag("Item"))//item
      //  {
      //      Debug.Log("ItemGEt");
       //     FindObjectOfType<InventoryManager>().AddItem(collsion.gameObject.GetComponent<ItemObject>().item, 1);

   //        }
    }
}
