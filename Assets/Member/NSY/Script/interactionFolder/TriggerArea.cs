using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using DM.Inven;
using Game.NPC;
public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    //private sign NPCsign;

    public GameObject MapImage;
    GameObject scanObject;


    /*int id;
    bool isId;
    private void Start()
    {
        id = NPCsign.SignID;
        isId = NPCsign.SignIsid;
    }
    private void Update()
    {
     
    }*/


    private void OnTriggerEnter(Collider collsion)
    {      
        if (collsion.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌함");

            if (Input.GetKeyDown(KeyCode.R))
            {
                MapImage.SetActive(true);
            }
        }
    }
}
