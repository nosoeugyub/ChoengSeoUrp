﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NSY.Manager;
public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] Animator fadeAnim;

    public void Start()
    {
        StartCoroutine(IFadeIn());
        EventManager.EventActions[(int)EventEnum.FadeOut] = FadeOut;
        EventManager.EventActions[(int)EventEnum.FadeIn] = FadeIn;

    }

    public void FadeOut()
    {
        StartCoroutine(IFadeOut());
    }
    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }
    public void LoadSceneString(string scenename)
    {
        if (scenename == "CreditDemo")
            StartCoroutine(LoadSceneLong(scenename));
        else
            SceneManager.LoadScene(scenename);
    }

    IEnumerator LoadSceneLong(string scenename )
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(scenename);
    }
    public void LoadSceneFadeString(string scenename)
    {
        StartCoroutine(LoadSceneFadeStringCo(scenename));
    }
    IEnumerator LoadSceneFadeStringCo(string scenename)
    {
        yield return IFadeOut();
        LoadSceneString(scenename);
    }
    IEnumerator IFadeOut()
    {
        yield return new WaitForSeconds(1f);
        fadeAnim.SetTrigger("whitescreen");
        yield return new WaitForSeconds(3f);
    }
    IEnumerator IFadeIn()
    {
        //yield return new WaitForSeconds(1f);
        fadeAnim.SetTrigger("startwhitescreen");
        yield return new WaitForSeconds(3f);
    }
    public void EndGame()
    {
        StartCoroutine(Quit());
    }
    IEnumerator Quit()
    {
        yield return IFadeOut();
        Application.Quit();
    }
    public void slbal(int scenenuber)
    {
        SceneManager.LoadScene(scenenuber);
    }
}
