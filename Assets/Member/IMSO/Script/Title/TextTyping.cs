using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTyping : MonoBehaviour
{
    public Text text; //텍스트
    [TextArea]
    public string[] dialog; //대사
    int dialogIndex; //대사 인덱스
    public float duration = 2f; //타이핑이 끝나고 다음 대사 출력 시작까지 대기할 시간
    bool isEndTyping; //타이핑이 끝났는지
    public float TypingPerSecond = 0.05f;//타이핑 속도

    public GameObject TextImage;

    private void Start()
    {
        text.text = dialog[dialogIndex];
        StartDialog();
    }

    //타이핑 효과 시작
    void StartDialog()
    {
        StartCoroutine(Dialog());
    }

    //타이핑 효과
    IEnumerator Dialog()
    {
        //타이핑 시작
        isEndTyping = false;

        //초기화
        text.text = "";

        string sentence = dialog[dialogIndex];

        //타이핑 효과
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;

            yield return new WaitForSeconds(TypingPerSecond);
        }

        isEndTyping = true;

        yield return new WaitForSeconds(duration);
    }

    //화면 클릭
    public void OnClickScreen()
    {
        if (!isEndTyping)
            return;

        dialogIndex++;

        if (dialogIndex >= dialog.Length)
        {
            TextImage.SetActive(false);
        }
        else
        {
            StartDialog();
        }

    }
}