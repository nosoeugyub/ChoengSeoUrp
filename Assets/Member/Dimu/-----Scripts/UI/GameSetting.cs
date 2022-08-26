using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
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

    public void ExitGame()
    {
        Application.Quit();
    }

}
