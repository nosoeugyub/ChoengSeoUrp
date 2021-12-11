using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;

public class CollionCheck : MonoBehaviour
{

          
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) //충돌한 물체가 리지드바디가 없거나 이즈 키네메틱이 되있더면 무시해라
            return;

        if (hit.moveDirection.y < -0.3F)//땅 은 무시해라
            return;
        //충돌검사
        if (hit.gameObject.CompareTag("signNPC"))//처번째 퀘스트 팻말
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Manager.Instance.OnFirstQuest();
                Debug.Log("팻말 퀘스트 시작해");
            }
        }
        if (hit.gameObject.CompareTag("Item"))//item
        {
                Debug.Log("ItemGEt");
            FindObjectOfType<InventoryManager>().AddItem(hit.gameObject.GetComponent<ItemObject>().item, 1);

        }
    }

}

