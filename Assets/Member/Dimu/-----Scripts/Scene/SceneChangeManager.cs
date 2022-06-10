using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;
    public void LoadSceneString(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void LoadSceneFadeString(string scenename)
    {
        StartCoroutine(Fade(scenename));
    }
    IEnumerator Fade(string scenename)
    {
        fadeAnim.SetTrigger("whitescreen");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scenename);
    }
}
