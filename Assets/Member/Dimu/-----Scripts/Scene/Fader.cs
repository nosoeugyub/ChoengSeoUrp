using NSY.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;
    [SerializeField] Image fadeImg;

    public void Start()
    {
        StartCoroutine(IFadeIn());
        DIalogEventManager.EventActions[(int)EventEnum.FadeOut] = FadeOutEvent;
        DIalogEventManager.EventActions[(int)EventEnum.FadeIn] = FadeInEvent;
    }

    public void FadeOut(Color color, float speed)
    {
        StartCoroutine(IFadeOut(color, speed));
    }
    public void FadeIn(Color color, float speed)
    {
        StartCoroutine(IFadeIn(color, speed));
    }
    public IEnumerator IFadeOut(Color color, float speed)
    {
        fadeImg.color = color;
        fadeImg.raycastTarget = true;
        fadeAnim.ResetTrigger("whitescreen");
        fadeAnim.ResetTrigger("startwhitescreen");
        fadeAnim.SetTrigger("whitescreen");
        fadeAnim.speed = 1 / speed;
        Debug.Log(fadeAnim.speed);
        Debug.Log(speed);
        yield return new WaitForSeconds(speed);
    }
    public IEnumerator IFadeIn(Color color, float speed)
    {
        fadeImg.color = color;
        fadeImg.raycastTarget = true;
        fadeAnim.ResetTrigger("startwhitescreen");
        fadeAnim.ResetTrigger("whitescreen");
        fadeAnim.SetTrigger("startwhitescreen");
        fadeAnim.speed = 1 / speed;
        yield return new WaitForSeconds(speed);
        fadeImg.raycastTarget = false;
    }

    //event method//////////////////////////////////////////

    public void FadeOutEvent()
    {
        StartCoroutine(IFadeOut());
    }
    public void FadeInEvent()
    {
        StartCoroutine(IFadeIn());
    }
    public IEnumerator IFadeOut()
    {
        fadeImg.raycastTarget = true;
        //yield return new WaitForSeconds(1f);
        fadeAnim.ResetTrigger("whitescreen");
        fadeAnim.ResetTrigger("startwhitescreen");
        fadeAnim.SetTrigger("whitescreen");
        fadeAnim.speed = 1f;
        yield return new WaitForSeconds(fadeAnim.speed);
    }
    public IEnumerator IFadeIn()
    {
        fadeImg.raycastTarget = true;
        fadeAnim.ResetTrigger("startwhitescreen");
        fadeAnim.ResetTrigger("whitescreen");
        fadeAnim.SetTrigger("startwhitescreen");
        fadeAnim.speed = 1f;
        yield return new WaitForSeconds(fadeAnim.speed);
        fadeImg.raycastTarget = false;
    }
}
