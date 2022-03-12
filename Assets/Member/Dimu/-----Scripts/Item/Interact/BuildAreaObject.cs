using UnityEngine;

public class BuildAreaObject : MonoBehaviour, IInteractable
{
    [SerializeField] BuildState buildState;

    [Tooltip("이 오브젝트를 채집할 수 있는 도구 타입")]
    [SerializeField] InItemType toolType;

    public string CanInteract()
    {
        return "건물 상호작용";
    }

    public void OnBuildMode(UnityEngine.UI.Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }

        //건축물 상호작용 인덱스 체크

        //Set Event Methods
        if (buildState == BuildState.NotFinish)
        {
            buttons[0].onClick.AddListener(() =>
            {
                print("1. Build Building");
                //1. Build Building
                //ex) BuildMode = true; Like your script 'BuildingBlock'
            });
            buttons[1].onClick.AddListener(() =>
            {
                print("2. break Building");
                //2. break Building
            });
            buttons[2].onClick.AddListener(() =>
            {
                print("3. Finish Building");
                //3. Finish Building
            });
        }
        else if (buildState == BuildState.Finish)
        {
            buttons[0].onClick.AddListener(() =>
            {
                print("1. Repair Building");
                //1. Repair Building
            });
            buttons[1].onClick.AddListener(() =>
            {
                print("2. break Building");
                //2. break Building
            });
            buttons[2].gameObject.SetActive(false);
        }
    }

    public Transform ReturnTF()
    {
        return transform;
    }

    public void SetBuildingState(BuildState buildstate)
    {
        buildState = buildstate;
    }
}
public enum BuildState { NotFinish, Finish }
