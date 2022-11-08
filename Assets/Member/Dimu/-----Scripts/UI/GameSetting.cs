using NSY.Player;
using StylizedWater;
using System.Collections;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] PlanarReflections reflection;
    [SerializeField] Fader fader;

    PlayerInput.InputEvent savedelegate;

    private void Start()
    {
        PlayerInput.OnPressExitDown = SettingUIONOff;
    }

    public void SettingUIONOff()
    {
        if (!settingUI.activeSelf)
        {
            savedelegate = PlayerInput.OnPressExitDown;
            PlayerInput.OnPressExitDown = SettingUIONOff;
        }
        else
        {
            //건축 시 예외처리 필요
            PlayerInput.OnPressExitDown = savedelegate;
        }
        settingUI.SetActive(!settingUI.activeSelf);
    }
    public void ClickCloseSetting()
    {
        PlayerInput.OnPressExitDown = savedelegate;
        settingUI.SetActive(false);
    }
    public void ReflectionONOff(bool value)
    {
        StartCoroutine(ReflectionWithFader());
    }
    IEnumerator ReflectionWithFader()
    {
        yield return fader.IFadeOut(Color.white, 0.3f);
        reflection.enabled = !reflection.isActiveAndEnabled;
        yield return new WaitForSeconds(0.5f);
        yield return fader.IFadeIn(Color.white, 0.3f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
