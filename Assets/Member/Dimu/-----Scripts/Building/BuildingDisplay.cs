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
        void Start()
        {

        }
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

        internal void SetButtonEvent(params Action[] ps)
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