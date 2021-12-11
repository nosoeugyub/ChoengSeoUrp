using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        //Talk Data
        //NPC : 1000 , Object 100, 200
        talkData.Add(1000, new string[] {"아", "못자서 죽을거 같아요"});

        talkData.Add(100, new string[] { "연구실은 102이다." });

        talkData.Add(200, new string[] { "누군가 당근을 던지고 간 자리이다." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "박카스를 가져왔구나", "랩장선배에게 가면 더 좋은 걸 얻을 수 있을거야" });

        talkData.Add(11 + 2000, new string[] { "선배는 포도당 비타민을 주었다." });
    }
    //지정된 대화 문장을 반환하는 함수
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
