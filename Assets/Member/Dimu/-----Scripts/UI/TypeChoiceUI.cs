using System;
using TMPro;
using UnityEngine;

public class TypeChoiceUI : MonoBehaviour
{
    MaterialChoiceUI materialChoiceUI;
    [SerializeField] TextMeshProUGUI typeText;

    [SerializeField] EEEEE[] ees;
    int index;

    private void Awake()
    {
        materialChoiceUI = FindObjectOfType<MaterialChoiceUI>();
    }
    private void Start()
    {
        if (ees == null)
        {
            ees = new EEEEE[2];

            ees[0] = new EEEEE();
            ees[1] = new EEEEE();
        }
        ees[0].type = typeof(BuildMaterial);
        ees[0].text = "재질";
        ees[0].elemants = new dynamic[6];
        ees[0].elemants[0] = BuildMaterial.Grass;
        ees[0].elemants[1] = BuildMaterial.Iron;
        ees[0].elemants[2] = BuildMaterial.Paper;
        ees[0].elemants[3] = BuildMaterial.Sand;
        ees[0].elemants[4] = BuildMaterial.Stone;
        ees[0].elemants[5] = BuildMaterial.Wood;

        ees[1].type = typeof(BuildShape);
        ees[1].text = "모양";
        ees[1].elemants = new dynamic[5];
        ees[1].elemants[0] = BuildShape.Square;
        ees[1].elemants[1] = BuildShape.Circle;
        ees[1].elemants[2] = BuildShape.Rectangle;
        ees[1].elemants[3] = BuildShape.Triangle;
        ees[1].elemants[4] = BuildShape.Etc;
        //ees[0].elemants[5] = Activator.CreateInstance(ees[0].type);

        Init();
        UpdateTextUI();
    }
    private void Init()
    {
        index = 0;
        materialChoiceUI.Init(ees.Length);
    }
    private void UpdateTextUI()
    {
        typeText.text = ees[index].text;
        materialChoiceUI.UpdateListUI(ees[index], index);
    }
    public void UpDownIndex(bool up)
    {
        if (up)
        {
            index += 1;
            if (index > ees.Length - 1)
                index = 0;
        }
        else
        {
            index -= 1;
            if (index < 0)
                index = ees.Length - 1;
        }
        UpdateTextUI();
    }
}
[Serializable]
public class EEEEE
{
    [SerializeField] public string text;
    [SerializeField] public Sprite[] images;
    [SerializeField] public Type type;
    [SerializeField] public dynamic[] elemants;
}