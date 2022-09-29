using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NPCNoticeUI : MonoBehaviour
{
    [SerializeField] Sprite[] season;
    [SerializeField] NpcNoticeEntity[] animals;
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    [SerializeField] GameObject informUI;
    [SerializeField] TextMeshProUGUI informUICheckText;
    [SerializeField] TextMeshProUGUI informUITutoText;
    bool isFirstUIOn;
    [SerializeField] NPCManager npcManager;

    [TextArea]
    [SerializeField] string informTextFormat;
    [TextArea]
    [SerializeField] string failInformTextFormat;


    private void Start()
    {
        isFirstUIOn = true;
        informUI.SetActive(false);
        for (int i = 1; i < npcManager.NpcTfs.Length; i++)
        {
            npcManager.NpcTfs[i].Npctf.UIOnEvent = SetAnimalImgOn;
            npcManager.NpcTfs[i].Npctf.UIUpdateEvent = UpdateNoticeColor;
        }
        for (int i = 1; i < animals.Length; i++)
        {
            animals[i].SetInitColor(onColor, offColor);
            animals[i].SetinformUIOnOffEvent = InformUIOnOff;
            animals[i].UpdateText = UpdateText;
        }
    }
    public void InformUIOnOff(bool isuion, bool ishavenextdialog, Vector3 movePos, string npctext)
    {
        informUI.SetActive(isuion);

        if (!isFirstUIOn)
            informUITutoText.gameObject.SetActive(false);

        if (isuion)
        {
            UpdateText(ishavenextdialog, npctext);

            informUI.transform.position = movePos;
        }
    }
    public void UpdateText(bool ishavenextdialog, string npctext)
    {
        if (ishavenextdialog)
            informUICheckText.text = string.Format(informTextFormat, npctext);
        else
            informUICheckText.text = string.Format(failInformTextFormat, npctext);
    }
    public void SetAnimalImgOn(int npcnum)
    {
        if (isFirstUIOn)
            StartCoroutine(DelayFirstMethod(npcnum));

        animals[npcnum].gameObject.SetActive(true);
        animals[npcnum].StartAnimation();
        if (npcManager.NpcTfs[npcnum].Npctf.MyHouse)
            animals[npcnum].SetSeasonSprite(season[npcManager.NpcTfs[npcnum].Npctf.MyHouse.Seasonnum]);
    }

    IEnumerator DelayFirstMethod(int npcnum)
    {
        yield return new WaitForSeconds(0.1f);
        animals[npcnum].FirstUIOnMethod();
        isFirstUIOn = false;
    }

    public void UpdateNoticeColor(int npcnum, DialogMarkType dialogMarkType)
    {
        animals[npcnum].UpdateNoticeColor(dialogMarkType);
        animals[npcnum].StartAnimation();
    }
}
