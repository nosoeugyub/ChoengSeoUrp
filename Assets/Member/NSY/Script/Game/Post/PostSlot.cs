using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace NSY.Iven
{
    public class PostSlot : MonoBehaviour
    {
        [Header("획득 되고안되고 색깔차이")]
        [SerializeField]
        private Button PostBtn;
        private Color AddColor = new Color(1, 1, 1, 1);
        //  private Color disabledColor = new Color(1, 1, 1, 0);
        private Color NoneAddColor = new Color(1, 1, 1, 0f);


        [Header("할당될 컴포넌트들")]
        [SerializeField] public Image postImage;   
        [SerializeField] public Text posttext;
        public GameObject PostOBJ;

        [Header("읽음표시 배열")]
        [SerializeField]
        public GameObject[] postRusult;


        [Header("안의 내용 오브젝트")]
        [SerializeField]
        private Image _PostContentsImg;
        public Image PostContentsImg
        {
            get
            {
                return _PostContents;
            }
            set
            {
                _PostContents = value;
                if (_post == null)
                {
                    PostOBJ = null;
                }
                else
                {
                    PostOBJ = _post._PostContents;
                    _PostContents = PostOBJ ;
                }

            }
        }


        [Header("할당될 오브젝트")]
        [SerializeField]
        private Post _post;
        public Post post
        {
            get
            {
                return _post;
            }
            set
            {
                _post = value;
                if (_post == null)
                {
                    postImage.sprite = null;
                    postImage.color = NoneAddColor;

                }
                else
                {
                    postImage.sprite = _post._PostImage;
                    postImage.color = AddColor;
                    PostBtn.image.color = AddColor;
                }
            }
        }

        private Text _PostText;
        public Text PostText
        {
            get { return _PostText; }

            set
            {
                _PostText = value;
                if (_post == null)
                {
                    posttext.text = null;
                }
                else
                {
                    _PostText.text = _post._Posttext;
                    posttext = _PostText;
                }
            }
        }

        public void OnValidate()
        {

            if (_post == null)
            {
                gameObject.name = "None Slot";
                PostBtn.image.color = NoneAddColor;
            }

            if (postImage == null)
            {
                postImage = GetComponent<Image>();
            }
            if (posttext == null)
            {
                posttext = GetComponent<Text>();
            }
            post = _post;
            PostText = _PostText;
            PostContents = _PostContents;
        }

  

    }

}
