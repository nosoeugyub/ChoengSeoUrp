using UnityEngine;

public class MagnifyObject : ItemObject
{
    [SerializeField] GameObject glassSpeechBubble;

    private void Awake()
    {
        base.Awake();
        glassSpeechBubble = Instantiate(Resources.Load("Object/glassSpeechBubble") as GameObject, this.transform);
        glassSpeechBubble.SetActive(false);
    }
    public override int CanInteract()
    {
        return (int)CursorType.Mag;
    }
    public void InstantiateBubble()
    {
        glassSpeechBubble.SetActive(true);
    }
    public bool CheckBubble(Animator animator)
    {
        //if (handitem.InItemType != InItemType.MagnifyingGlass)
        //{
        //    print("다른 도구로 시도해주세요.");
        //    return false;
        //}
        if (!glassSpeechBubble.activeSelf)
        {
            print("돋보기가 활성화되지 않았습니다.");
            return false;
        }
        animator.SetBool("isMagnifying", true);
        glassSpeechBubble.SetActive(false);
        ObjectManager.CheckBubble();
        //확률로 레시피 획득 구문
        return true;
    }
}