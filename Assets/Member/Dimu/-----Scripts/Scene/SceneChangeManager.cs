using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] Fader fader;
    Color loadscenecolor;
    float loadscenespeed;
    [SerializeField] GameEvent playerMoveOnEvent;
    [SerializeField] GameEvent playerMoveOffEvent;

    [SerializeField] GameEvent playerCanInteractEvent;
    [SerializeField] GameEvent playerCantInteractEvent;
    private void Start()
    {
        loadscenecolor = Color.white;
        loadscenespeed = 1;
        StartCoroutine(StartSceneFade());
    }
    public IEnumerator StartSceneFade()
    {
        playerMoveOffEvent.Raise();
        playerCantInteractEvent.Raise();
        yield return fader.IFadeIn();
        playerMoveOnEvent.Raise();
        playerCanInteractEvent.Raise();
    }
    public void LoadSceneFadeString(string scenename)
    {
        StartCoroutine(LoadSceneFadeStringCo(scenename));
    }

    IEnumerator LoadSceneFadeStringCo(string scenename)
    {
        playerMoveOffEvent.Raise();
        yield return fader.IFadeOut(loadscenecolor,loadscenespeed);
        LoadSceneAsString(scenename);
    }

    private void LoadSceneAsString(string scenename)
    {
        if (scenename == "CreditDemo")
            StartCoroutine(LoadSceneLong(scenename));
        else
            SceneManager.LoadScene(scenename);
    }

    IEnumerator LoadSceneLong(string scenename)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(scenename);
    }

    public void EndGame()
    {
        StartCoroutine(Quit());
    }
    IEnumerator Quit()
    {
        playerMoveOffEvent.Raise();
        yield return fader.IFadeOut(loadscenecolor, loadscenespeed);
        Application.Quit();
    }
    public void LoadSceneAsNumber(int scenenuber)
    {
        SceneManager.LoadScene(scenenuber);
    }
}
