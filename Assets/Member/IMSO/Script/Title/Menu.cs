using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void introToMain()
    {
        Debug.Log("메인 게임");
        SceneManager.LoadScene("Main");
    }
    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        SceneManager.LoadScene("Intro");
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
}
