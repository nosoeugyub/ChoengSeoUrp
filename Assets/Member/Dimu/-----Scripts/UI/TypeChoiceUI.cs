using System;
using TMPro;
using UnityEngine;

public class TypeChoiceUI : MonoBehaviour
{
    MaterialChoiceUI materialChoiceUI;
    [SerializeField] TextMeshProUGUI typeText;

    [SerializeField] BuildingMaterialsInfo[] buildmatinfo;
    int index;

    private void Awake()
    {
        materialChoiceUI = FindObjectOfType<MaterialChoiceUI>();

        if (buildmatinfo == null)
        {
            buildmatinfo = new BuildingMaterialsInfo[2];

            buildmatinfo[0] = new BuildingMaterialsInfo();
            buildmatinfo[1] = new BuildingMaterialsInfo();
        }
        buildmatinfo[0].type = typeof(BuildMaterial);
        buildmatinfo[0].name = "재질";
        buildmatinfo[0].elemants = new dynamic[6];
        buildmatinfo[0].elemantsname = new string[6];
        buildmatinfo[0].elemants[0] = BuildMaterial.Grass;
        buildmatinfo[0].elemantsname[0] = "풀";
        buildmatinfo[0].elemants[1] = BuildMaterial.Iron;
        buildmatinfo[0].elemantsname[1] = "철";
        buildmatinfo[0].elemants[2] = BuildMaterial.Paper;
        buildmatinfo[0].elemantsname[2] = "종이";
        buildmatinfo[0].elemants[3] = BuildMaterial.Sand;
        buildmatinfo[0].elemantsname[3] = "모래";
        buildmatinfo[0].elemants[4] = BuildMaterial.Stone;
        buildmatinfo[0].elemantsname[4] = "돌";
        buildmatinfo[0].elemants[5] = BuildMaterial.Wood;
        buildmatinfo[0].elemantsname[5] = "나무";

        buildmatinfo[1].type = typeof(BuildShape);
        buildmatinfo[1].name = "모양";
        buildmatinfo[1].elemants = new dynamic[5];
        buildmatinfo[1].elemantsname = new string[5];
        buildmatinfo[1].elemants[0] = BuildShape.Square;
        buildmatinfo[1].elemantsname[0] = "사각형";
        buildmatinfo[1].elemants[1] = BuildShape.Circle;
        buildmatinfo[1].elemantsname[1] = "원형";
        buildmatinfo[1].elemants[2] = BuildShape.Rectangle;
        buildmatinfo[1].elemantsname[2] = "직사각형";
        buildmatinfo[1].elemants[3] = BuildShape.Triangle;
        buildmatinfo[1].elemantsname[3] = "삼각형";
        buildmatinfo[1].elemants[4] = BuildShape.Etc;
        buildmatinfo[1].elemantsname[4] = "그 외 모양";
        Init();
    }
    private void Start()
    {
        //ees[0].elemants[5] = Activator.CreateInstance(ees[0].type);
        UpdateTextUI();
    }
    private void Init()
    {
        index = 0;
        materialChoiceUI.Init(buildmatinfo.Length);
    }
    public void UpdateTextUI() //nowbuildingblock 업데이트 후 호출해야 함 흠..
    {
        typeText.text = buildmatinfo[index].name;
        materialChoiceUI.UpdateListUI(buildmatinfo[index], index);
    }
    public void UpDownIndex(bool up)
    {
        if (up)
        {
            index += 1;
            if (index > buildmatinfo.Length - 1)
                index = 0;
        }
        else
        {
            index -= 1;
            if (index < 0)
                index = buildmatinfo.Length - 1;
        }
        UpdateTextUI();
    }
}
[Serializable]
public class BuildingMaterialsInfo
{
    [SerializeField] public string name;
    [SerializeField] public Sprite[] images;
    [SerializeField] public Type type;
    [SerializeField] public dynamic[] elemants;
    [SerializeField] public string[] elemantsname;

    public void Setdynamicelemants(params dynamic[] es)
    {

    }
}