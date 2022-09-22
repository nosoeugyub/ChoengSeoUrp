using System;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Building
{
    public class BuildingDisplay : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Button buildOffUi;
        [SerializeField] GameObject cancelUi;
        [SerializeField] GameObject buildingTutorialImg;

        public void BuildDisplayOn(bool isOn)
        {
            BuildOffUiState(isOn);
            TutoUIState(isOn);
        }

        public void CancelUIState(bool isOn)
        {
            cancelUi.SetActive(isOn);
        }

        private void BuildOffUiState(bool isOn)
        {
            buildOffUi.gameObject.SetActive(isOn);
        }

        public void SetBuildModeOffButtonEvent(params Action[] ps)
        {
            foreach (Action action in ps)
            {
                buildOffUi.onClick.AddListener(() => action());
            }
        }

        private void TutoUIState(bool isOn)
        {
            buildingTutorialImg.SetActive(isOn);
        }
    }
}