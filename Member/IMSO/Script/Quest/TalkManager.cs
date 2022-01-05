//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TalkManager : MonoBehaviour
//{
//    Dictionary<int, string[]> talkData;

//    void Awake()
//    {
//        talkData = new Dictionary<int, string[]>();
//        GenerateData();
//    }

//    void GenerateData()
//    {
//        //Sign
//        talkData.Add(1, new string[] { "마을 이정표 팻말" });
//        //Talk Data
//        //NPC : 1000 , Sign 1
//        talkData.Add(1000, new string[] {"어디서 온 청년인가?", "마을이 좀 엉망이지?", "태풍 때문에 마을에 타격이 크다네"});
//    }
//    //지정된 대화 문장을 반환하는 함수
//    public string GetTalk(int id, int talkIndex)
//    {
//       if (talkIndex == talkData[id].Length)
//            return null;
//        else
//            return talkData[id][talkIndex];
//    }
//}
