using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Intro1()
    {
        SceneManager.LoadScene("Intro1");
    }
    public void Intro2()
    {
        SceneManager.LoadScene("Intro2");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
  
    public void introToMain()
    {
        Debug.Log("메인 게임");
        SceneManager.LoadScene("Main");
    }
}
