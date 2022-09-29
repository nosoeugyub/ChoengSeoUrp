using DM.Quest;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField] Text _questNameText;
    [SerializeField] Text _descriptionText;
    [SerializeField] Image _npcImg;
    [SerializeField] Transform moveChild;
    Vector3 moveChildorg;
    [SerializeField] Ease ease;
    [SerializeField] float moveDuration;
    [SerializeField] GameObject boomParticle;
    private void Start()
    {
        boomParticle.SetActive(false);
        //moveChildorg = moveChild.transform.position;
    }
    public void OnEnable()
    {
        //moveChild.transform.position = moveChildorg;
        moveChild.transform.DOLocalMove(Vector3.zero, moveDuration).SetEase(ease);
    }
    public void SetDisable()
    {
        boomParticle.SetActive(true);
        boomParticle.transform.SetParent(null);
        gameObject.SetActive(false);
    }
    public void UpdateQuestInfoUI(QuestData questData, Sprite npcSprite)
    {
        _questNameText.text = string.Format("{0}", questData.questName);
        _descriptionText.text = string.Format(questData.description);
        //qui.transform.Find("ProgressText").GetComponent<Text>().text
        //    = string.Format(questData.description);
        _npcImg.sprite = npcSprite;
    }
}

