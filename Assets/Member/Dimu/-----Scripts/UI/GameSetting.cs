using StylizedWater;
using System.Collections;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] PlanarReflections reflection;
    [SerializeField] Fader fader;
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SettingUIONOff();
        }
    }

    public void SettingUIONOff()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }
    public void ReflectionONOff(bool value)
    {
        StartCoroutine(ReflectionWithFader());
    }
    IEnumerator ReflectionWithFader()
    {
        yield return fader.IFadeOut(Color.white,0.3f);
        reflection.enabled = !reflection.isActiveAndEnabled;
        yield return new WaitForSeconds(0.5f);
        yield return fader.IFadeIn(Color.white,0.3f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
