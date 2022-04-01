using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Iven
{
    public class PostEvent : MonoBehaviour
    {
        [SerializeField]
        PostPanel n_PostPanel;
        [SerializeField]
        PostSlot n_PostSlot;
        public bool isRead;



        public void BtnClickPost()
        {
            if (!n_PostPanel.PostResultImg.enabled)
            {
                StartCoroutine(ChageImg());
              
            }
            StopCoroutine(ChageImg());
        }
        IEnumerator ChageImg()
        {
            n_PostPanel.PostResultImg.enabled = false;
            n_PostPanel.closebtn.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            n_PostPanel.PostResultImg.enabled = true;
            n_PostPanel.PostResultImg.sprite = n_PostSlot.PostImg.sprite;
            yield return new WaitForSeconds(0.1f);
            //읽었음
            for (int i = 0; i < n_PostPanel.AddPostList.Count; i++)
            {
                transform.SetSiblingIndex(i);
            }
            n_PostSlot.PostBtn.image.color = n_PostSlot.ReadColor;

        }
      




    }


}

