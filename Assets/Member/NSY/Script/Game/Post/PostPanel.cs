using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Iven
{
  
    public class PostPanel : MonoBehaviour
    {
        [SerializeField] Transform PostSlotsParent;
        [SerializeField] private List<PostSlot> postslot;
        [SerializeField] private List<Post> Post; //추가되면 들어갈 우편목록

        private void OnValidate()
        {
            if (PostSlotsParent != null)
            {
                 PostSlotsParent.GetComponentsInChildren(includeInactive: true, result: postslot);
               // postslot = PostSlotsParent.GetComponentsInChildren<PostSlot>();
            }
        }





        public bool AddPost(Post post)
        {
            for (int i = 0; i < postslot.Count; i++)
            {
                if (postslot[i].post == null)
                {
                    postslot[i].post = post;
                    postslot[i].posttext.text =post._Posttext;
                    postslot[i].gameObject.name = post._PostName.ToString() + "Slot";
                    return true;
                }
               
            }
            return false;
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
