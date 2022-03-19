using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnIven : MonoBehaviour
{

    [SerializeField] GameObject EquionmentPanel;
    [SerializeField] GameObject iteminfoPanel;
    [SerializeField] GameObject CraftPanel;

    bool isInven;
    bool Craft;

    public void IvenBtnClick()
    {
        isInven = true;
        Craft = false;
        if (isInven)
        {
            CraftPanel.SetActive(false);
            EquionmentPanel.SetActive(true);
            iteminfoPanel.SetActive(true);
        }
    }
    public void CraftBtnClick()
    {
        isInven = false;
        Craft = true;
        if (Craft)
        {
            CraftPanel.SetActive(true);
            EquionmentPanel.SetActive(false);
            iteminfoPanel.SetActive(false);
        }
    }
}
