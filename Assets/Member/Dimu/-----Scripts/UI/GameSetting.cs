using StylizedWater;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] PlanarReflections reflection;
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
        reflection.enabled = !reflection.isActiveAndEnabled;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
