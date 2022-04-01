using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace NSY.Iven
{
  
    public class PostPanel : MonoBehaviour
    {
        [SerializeField] public GameObject closebtn;

        //부모 오브젝트
        public Transform ParentObj;
        //슬로오브젝트
        public PostSlot Slot;

       
        //이벤트
        public Transform PostRusultTransform;
        public GameObject[] PostRusultPrafab;
        public Image PostResultImg;


        PostSlot postslotss;
        [SerializeField] Transform PostSlotsParent;

        [SerializeField] public List<PostSlot> postslot;
        [Header("우편 목록")]
        [SerializeField] private Post[] Post; //우편목록 
        [Header("안읽은 우편함")]
        public List<Post> AddPostList = new List<Post>(); // 안읽은 우편함

        private void OnValidate()
        {
            if (PostSlotsParent != null)
            {
                 PostSlotsParent.GetComponentsInChildren(includeInactive: true, result: postslot);
              
            }
        }
    
       
        public bool AddPost(Post post)
        {
            //var n_Slot = Instantiate(Slot);
            //n_Slot.transform.SetParent(ParentObj.transform, false);
            //postslot.Add(n_Slot);
            for (int i = 0; i < postslot.Count; i++)
            {
                if (postslot[i].post == null)
                {
                    

                    postslot[i].post = post;
                    postslot[i].posttext.text = post._Posttext;
                    postslot[i].PostImg = post._Postimg;
                    postslot[i].gameObject.name = post._PostName.ToString();
                    postslot[i].gameObject.transform.SetAsFirstSibling();
                    

                    for (int j = 0; j < Post.Length; j++)
                    {
                        if (postslot[i].post == Post[j])
                        {
                            AddPostList.Add(Post[j]); //안읽은 메시지
                         
                            Post[j] = null;
                           
                        }
                    }
                    return true;
                }

            }
           
            return false;
        }




   

       

        public void ClosePost()
        {
            closebtn.SetActive(false);
            PostResultImg.enabled =false;
            PostResultImg.sprite = null;
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                AddPost(Post[0]);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                AddPost(Post[3]);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                AddPost(Post[1]);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                AddPost(Post[2]);
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                AddPost(Post[4]);
            }
        }

    }

   
}
