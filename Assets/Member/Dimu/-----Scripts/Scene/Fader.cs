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
        StartCoroutine(IFadeIn(Color.white, 2));
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
}
