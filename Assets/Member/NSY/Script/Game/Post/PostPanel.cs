﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NSY.Iven
{
  
    public class PostPanel : MonoBehaviour
    {

        //이벤트
        public GameObject PostRusultTransform;
        public GameObject[] PostRusultPrafab;

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
            for (int i = 0; i < postslot.Count; i++)
            {
                if (postslot[i].post == null)
                {
                    postslot[i].post = post;
                    postslot[i].posttext.text = post._Posttext;
                    postslot[i].PostOBJ = post._PostContents;
                    post._PostNum++;
                   postslot[i].gameObject.name = post._PostName.ToString();
                   
                    for (int j = 0; j < Post.Length; j++)
                    {
                        if (postslot[i].post == Post[j])
                        {
                            Post[j] = null;
                        }
                    }
                    return true;
                }

            }
            return false;
        }


        public void AddContectPost()
        {

            for (int i = 0; i < postslot.Count; i++)
            {
                if (postslot[i].post != null)
                {
                   
                    GameObject PostImage = Instantiate(postslot[i].PostOBJ) as GameObject ;
                    PostImage.transform.SetParent(PostRusultTransform.transform, false);
                    postslot[i].PostOBJ.SetActive(true);
                }
            }
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
