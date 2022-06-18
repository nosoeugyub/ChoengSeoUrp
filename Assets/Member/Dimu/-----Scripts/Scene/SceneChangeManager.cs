using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NSY.Manager;
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
        
        yield return new WaitForSeconds(1f);
        fadeAnim.SetTrigger("whitescreen");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scenename);
    }

    public void slbal(int scenenuber)
    {
        SceneManager.LoadScene(scenenuber);
    }
}
