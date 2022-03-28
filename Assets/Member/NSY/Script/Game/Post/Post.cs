using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Iven
{
    [CreateAssetMenu(fileName ="Post")]//ScriptableObject를 상속받는 클래스를 스크립터블 오브젝트 어셋으로 생성 가능
    public class Post : ScriptableObject
    {
        //우편 데이터 이름
        [SerializeField]
        private string PostName;
        public string _PostName { get { return PostName; } }

        //우편 보낸 사람
        [SerializeField]
        private string Posttext;
        public string _Posttext { get { return Posttext; } }

        //우편 보낸사람 이미지
        [SerializeField]
        private Sprite PostImage;
        public Sprite _PostImage { get { return PostImage; } }

        //우편 내용 텍스트
        [SerializeField]
        private GameObject PostContents;
        public GameObject _PostContents { get { return PostContents; } }


        [SerializeField]
         public int _PostNum;
        public int PostNumP {  get { return _PostNum; } }
    }

}

