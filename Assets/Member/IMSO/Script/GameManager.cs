using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Manager;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;// 충돌체 오브젝트
    public bool isAction;
    public int talkIndex;
    //성엽 추가 변수
   
    //성엽========================================================
    private void Start()
    {
        Debug.Log("Action함수 출력");

            Manager.Instance.GoVillageQ += QAction;
        
      
    }
    private void OnDestroy()
    {
        
            Manager.Instance.GoVillageQ -= QAction;
        
       
    }
    //===========================================================================


    public void QAction(GameObject scanObj)
    {
        if(isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNPC);
        }
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNPC)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }
}
