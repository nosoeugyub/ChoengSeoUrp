using UnityEngine;
using UnityEngine.UI;

public class MaterialChoiceUI : MonoBehaviour
{
    int[] index;
    int nowindex;
    BuildingItemPercentUI buildingItemPercentUI;
    [SerializeField] Image image;
    BuildingMaterialsInfo nowlist;
    private void Awake()
    {
        buildingItemPercentUI = FindObjectOfType<BuildingItemPercentUI>();
    }

    internal void UpdateListUI(BuildingMaterialsInfo matinfo, int _index)
    {
        nowlist = matinfo;
        nowindex = _index;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        image.sprite = nowlist.images[index[nowindex]];
        print(nowlist.elemants[index[nowindex]]);
        buildingItemPercentUI.UpdatePercent(nowlist.elemants[index[nowindex]], nowlist.type, nowlist.elemantsname[index[nowindex]]);
    }

    public void UpDownIndex(bool up)
    {
        if (up)
        {
            index[nowindex] += 1;
            if (index[nowindex] > nowlist.elemants.Length - 1)
                index[nowindex] = 0;
        }
        else
        {
            index[nowindex] -= 1;
            if (index[nowindex] < 0)
                index[nowindex] = nowlist.elemants.Length - 1;
        }
        UpdateSprite();
    }

    internal void Init(int eLength)
    {
        index = new int[eLength];
        for (int i = 0; i < index.Length; i++)
        {
            index[i] = 0;
        }
    }
}
