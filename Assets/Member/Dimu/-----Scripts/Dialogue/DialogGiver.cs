using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;

public class DialogGiver : MonoBehaviour
{
    public string[] dialogDataList;//해당 캐릭터의 대화 데이터 파일명 모음
    public void StartDialog(int idx)
    {
        //SuperManager.Instance.dialogueManager.StartShowDialog(dialogDataList[idx]);
    }
}
