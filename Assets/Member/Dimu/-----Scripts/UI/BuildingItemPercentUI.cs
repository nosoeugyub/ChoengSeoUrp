using DM.Building;
using System;
using TMPro;
using UnityEngine;

public class BuildingItemPercentUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI percentnameText;
    [SerializeField] TextMeshProUGUI percentText;
    BuildingManager buildingManager;
    private void Awake()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
    }
    internal void UpdatePercent(dynamic dynamic, Type type, string name)
    {
        percentText.text = string.Format("{0}%", buildingManager.GetPercent(dynamic, type));
        percentnameText.text = name;
    }
}
