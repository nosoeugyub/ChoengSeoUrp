using UnityEngine;
using UnityEngine.UI;

namespace DM.Building
{
    public class BuildingBlueprint : MonoBehaviour //플레이어가 가지는 능력
    {
        [SerializeField]
        private GameObject buildFab;
 
        [SerializeField]
        private Button buildButton;
        private void Awake()
        {
            buildButton.onClick.AddListener(() =>
            {
                FindObjectOfType<BuildingPlace>().OnBuildMode(buildFab);
            });
        }
    }
}