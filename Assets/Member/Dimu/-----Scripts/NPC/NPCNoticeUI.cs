using NSY.Manager;
using UnityEngine;
using UnityEngine.UI;

public class NPCNoticeUI : MonoBehaviour
{
    [SerializeField] Sprite[] season;
    [SerializeField] Image[] animals;
    [SerializeField] Image[] animalSeason;
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;

    private void Start()
    {
        for (int i = 1; i < SuperManager.Instance.npcManager.NpcTfs.Length; i++)
        {
            SuperManager.Instance.npcManager.NpcTfs[i].Npctf.UIOnEvent =SetAnimalImgOnOff;
            SuperManager.Instance.npcManager.NpcTfs[i].Npctf.UIUpdateEvent = UpdateNotice;
        }
    }
    public void SetAnimalImgOnOff(int npcnum)
    {
        animals[npcnum].gameObject.SetActive(true);
        if(SuperManager.Instance.npcManager.NpcTfs[npcnum].Npctf.MyHouse)
        SetAnimalSeason(npcnum, SuperManager.Instance.npcManager.NpcTfs[npcnum].Npctf.MyHouse.Seasonnum);
    }
    public void SetAnimalSeason(int npcnum, int seasonnum)
    {
        animalSeason[npcnum].sprite = season[seasonnum];
    }

    public void UpdateNotice(int npcnum, DialogMarkType dialogMarkType)
    {
        if (dialogMarkType != DialogMarkType.None)
        {
            animals[npcnum].color = onColor;
        }
        else
            animals[npcnum].color = offColor;

    }
}
