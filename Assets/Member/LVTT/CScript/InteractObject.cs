using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TT.ObjINTERACT
    {
   
    public class InteractObject : MonoBehaviour
    {
        //Inventory inventory;

        //public BuildingMat MaterialType;
        //public string ItemName;
        //public GameObject ItemButton;
        //public SpriteRenderer ItemImage;


        public bool isinteracting;
        public float ShowUIDistant;
        [SerializeField] Transform ItemUI;
        public GameObject prefabUi;
        private GameObject TutUi=null;
        [SerializeField] Vector3 TriggerUIoffset;

        private GameObject ThePlayer;
        public bool TutUIisOn;
        InteractUI InteractUI;
     
        void Start()
        {
            TutUIisOn = false;
            //TutUi = Instantiate(prefabUi, ItemUI.transform);
            ThePlayer =GameObject.FindGameObjectWithTag("Player");
            InteractUI = FindObjectOfType<InteractUI>();
            // inventory = FindObjectOfType<Inventory>();
            //ItemImage.sprite = MaterialType.Mats_Image;
            //transform.name = MaterialType.name;
            //ItemName = MaterialType.name;
            //ItemButton = MaterialType.Inventory_Image;
            
        }

        void Update()
        {
            float DistantFromPlayer = Vector3.Distance(transform.position, ThePlayer.transform.position);

            







            if (ItemUI.childCount > 0)
            {
                TutUi.transform.position = Camera.main.WorldToScreenPoint(transform.position + TriggerUIoffset);



                float UiScaleValue = Vector3.Distance(transform.position, ThePlayer.transform.position);
                UiScaleValue = Mathf.Clamp(UiScaleValue, 0.1f, 0.5f);
                TutUi.transform.localScale = new Vector3(UiScaleValue, UiScaleValue, 0);

                if (DistantFromPlayer > ShowUIDistant)
                {
                    InteractUI.UnshowInteract();
                    foreach (Transform child in ItemUI.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    TutUIisOn = false;
                }


            }

            if (ItemUI.childCount <= 0)
            {
                if (DistantFromPlayer <= ShowUIDistant)
                {

                    if (!TutUIisOn)
                    {
                        if(!InteractUI.onInteractTrigger)
                        { InteractUI.ShowInteract();
                          }
                        TutUi = Instantiate(prefabUi, ItemUI.transform);
                        TutUIisOn = true;
                    }

                }
                if(DistantFromPlayer>ShowUIDistant)
                {

                }

             }

                //if (isinteracting)
                //    {
                //        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                //        {
                //           // ObjPickup();
                //        }
                //    }   
            }
       
        void UnshowTutUi()
        {
            foreach (Transform child in ItemUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }    

        //void ObjPickup()
        //{

        //    for (int i = 0; i < inventory.InventorySlotList.Length; i++)
        //    {
        //        if (!inventory.InventorySlotList[i].isFull)
        //        {
        //            if (inventory.InventorySlotList[i].haveItem)
        //            {
        //                if (inventory.InventorySlotList[i].curItemName != null)
        //                {

        //                    if (ItemName == inventory.InventorySlotList[i].curItemName)
        //                    {
                           
        //                        inventory.InventorySlotList[i].curItemNum += 1;
        //                        inventory.InventorySlotList[i].SlotStackTxt.text = inventory.InventorySlotList[i].curItemNum.ToString();
        //                        if (inventory.InventorySlotList[i].curItemNum == inventory.InventorySlotList[i].maxStack)
        //                        {
        //                            inventory.InventorySlotList[i].isFull = true;
        //                        }
        //                        Destroy(gameObject);
        //                        break;
        //                    }
        //                }
        //            }
        //            else if (!inventory.InventorySlotList[i].haveItem)
        //            {
        //                inventory.InventorySlotList[i].SlotStack.SetActive(true);
        //                inventory.InventorySlotList[i].haveItem = true;
        //                GameObject Itemimage = Instantiate(ItemButton, inventory.InventorySlotList[i].slot.transform, false);
        //                Itemimage.name = ItemButton.name;
        //                inventory.InventorySlotList[i].curItemNum += 1;
        //                inventory.InventorySlotList[i].curItemName = Itemimage.name;
        //                inventory.InventorySlotList[i].SlotStackTxt.text = inventory.InventorySlotList[i].curItemNum.ToString();
        //                Destroy(gameObject);
        //                break;
        //            }
        //        }

        //    }

        //}
       
    }
}

