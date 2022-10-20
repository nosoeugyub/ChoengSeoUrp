using DM.Event;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] Fader fader;
    Color loadscenecolor;
    float loadscenespeed;

    EventContainer eventContainer;

    private void Awake()
    {
        eventContainer = FindObjectOfType<EventContainer>();

    }
    private void Start()
    {
        loadscenecolor = Color.white;
        loadscenespeed = 1;
        StartCoroutine(StartSceneFade());
    }
    public IEnumerator StartSceneFade()
    {
        eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);
        eventContainer.RaiseEvent(GameEventType.playerCantInteractEvent);
        yield return fader.IFadeIn(loadscenecolor, loadscenespeed);
        eventContainer.RaiseEvent(GameEventType.playerMoveOnEvent);
        eventContainer.RaiseEvent(GameEventType.playerCanInteractEvent);
    }
    public void LoadSceneFadeString(string scenename)
    {
        StartCoroutine(LoadSceneFadeStringCo(scenename));
    }

    IEnumerator LoadSceneFadeStringCo(string scenename)
    {
        eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);
        yield return fader.IFadeOut(loadscenecolor, loadscenespeed);
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
        eventContainer.RaiseEvent(GameEventType.playerMoveOffEvent);
        yield return fader.IFadeOut(loadscenecolor, loadscenespeed);
        Application.Quit();
    }
    public void LoadSceneAsNumber(int scenenuber)
    {
        SceneManager.LoadScene(scenenuber);
    }
}
