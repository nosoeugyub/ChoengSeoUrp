using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;





public class CollionCheck : MonoBehaviour
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



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        





       Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) //충돌한 물체가 리지드바디가 없거나 이즈 키네메틱이 되있더면 무시해라
            return;

        if (hit.moveDirection.y < -0.3F)//땅 은 무시해라
            return;
   
        if (hit.gameObject.CompareTag("Item"))//item
        {
                Debug.Log("ItemGEt");
            FindObjectOfType<InventoryManager>().AddItem(hit.gameObject.GetComponent<ItemObject>().item, 1);

        }
        //
      
            if (hit.gameObject.CompareTag("signNPC"))//퀘스트
            {
                if (Input.GetKeyDown(KeyCode.R))
                {

                    Manager.Instance.OnFirstQuest(id, isId);
                    Debug.Log("팻말 퀘스트 시작해");


                }
            }
        
    }

}

