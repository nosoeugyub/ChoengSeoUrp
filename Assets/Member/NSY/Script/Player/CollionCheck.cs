using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;




public class CollionCheck : MonoBehaviour
{


    private void Update()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Rigidbody body = hit.collider.attachedRigidbody;
       // var Velocity = hit.rigidbody.velocity.magnitude;
      

        if (body == null || body.isKinematic) //충돌한 물체가 리지드바디가 없거나 이즈 키네메틱이 되있더면 무시해라
            return;

        if (hit.moveDirection.y < -0.3F)//땅 은 무시해라
            return;
   
       
        //


    

    }


}

