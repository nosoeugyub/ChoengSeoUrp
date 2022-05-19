using DM.NPC;
using DM.Quest;
using NSY.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Character
{ CheongSeo, Ejang, Walrus, Hen, Bee, Rabbit, Deer, Milkcow, Sheep, Length }
//청서 곰 닭 바코 벌 토끼 사슴 젖소 양

namespace DM.Dialog
{

    public class DialogueManager : MonoBehaviour
    {
        DialogData nowDialogData;//현재담겨있는 대화 데이터 
        int dialogLength;
        int nowSentenceIdx;
        public Transform partnerTf;
        public Transform[] npcTalkBubbleTfs;

        [Header("UI")]
        //public GameObject dialogUI;//대화창 조상
        public Button nextButton; //다음 버튼 >> 별로다
        public Text dialogText;
        public Text nameText;

        [Header("DialogInfos")]
        public List<DialogList> questDialogLists; //퀘스트 있는 대화
        public List<DialogList> dailydialogLists; //퀘스트 없는 대화
        public List<DialogList> buildDialogLists; //건축 피드백 대화

        [Header("InstanciatePrefab")]
        public GameObject textboxFab;//대화창 프리펩 //쪽지로 변하는것임!!

        GameObject nowOnFab;
        QuestData canClearqd;
        HouseNpc nowNpc;
        bool isTalking = false;
        float times = 0;

        QuestManager questManager;
        NPCManager npcManager;

        private void Awake()
        {
            questManager = SuperManager.Instance.questmanager;
            npcManager = FindObjectOfType<NPCManager>();
        }
        void Start()
        {
            EventManager.EventActions[1] = Test;

            FirstShowDialog(npcTalkBubbleTfs[(int)Character.CheongSeo].parent.GetComponent<HouseNpc>(), null, false, -1);
        }
        public HouseNpc GetNowNpc()
        {
            return nowNpc;
        }
        public void Test()
        {
            print("event test");
            times += Time.deltaTime;
            if (times > 3) { EventManager.EventAction -= EventManager.EventActions[1]; }
        }
        public bool FirstShowDialog(HouseNpc npc, Item handitem, bool isFollowPlayer, int isLike) //첫 상호작용 시 호출. 어떤 대화를 호출할지 결정
        {
            if (isTalking)
            {
                Debug.Log("대화중입니다.");
                return false;
            }

            isTalking = true;
            nowNpc = npc;
            partnerTf = npc.transform;
            nowSentenceIdx = 0;

            PlayerData.AddValue((int)npc.GetCharacterType(), (int)NpcBehaviorEnum.Interact, PlayerData.npcData, (int)NpcBehaviorEnum.length);

            StartShowDialog(handitem, isFollowPlayer, isLike); //파트너와 진행해야 하는 순서의 대화를 진행
            return true;
        }

        public void StartShowDialog(Item handitem, bool isFollowPlayer, int isLike)
        {
            Sentence[] ss = null;//대화뭉치를 담을 변수
            int sentenceState = -1;//대화뭉치 타입(수락0, 진행중1, 완료2)

            //현재 진행중인 퀘스트에서 자신과 상호작용 하는 내용의 퀘스트가 있는지?
            //있다면 해당 퀘스트의 CanClear 검사
            //클리어할 수 있다면 해당 퀘스트 클리어 처리 후 완료 대사를 ss에 넣는다.
            //dialogUI.SetActive(true);
            //Vector3 uiPos = new Vector3(partnerTf.position.x, partnerTf.position.y + 6, partnerTf.position.z);
            //dialogUI.transform.position = Camera.main.WorldToScreenPoint(uiPos);

            if (isFollowPlayer)
            {
                if (isLike == (int)BuildingLike.None) return;

                nowDialogData = buildDialogLists[(int)nowNpc.GetCharacterType()].dialogList[isLike];
                ss = nowDialogData.acceptSentenceInfo;
                dialogLength = nowDialogData.acceptSentenceInfo.Length;
                sentenceState = -1;
            }
            else
            {
                canClearqd = questManager.ReturnCanClearQuestRequireNpc((int)nowNpc.GetCharacterType());

                //List<QuestData> isAcceptedQuests = questManager.GetIsAcceptedQuestList(nowPartner);//진행중인 퀘스트
                //List<QuestData> canAcceptQuests = questManager.GetCanAcceptQuestList(nowPartner);//수락가능 퀘스트
                List<QuestData> isAcceptedQuests = questManager.GetIsAcceptedQuestList((int)nowNpc.GetCharacterType());//진행중인 대화 
                List<DialogData> canStartDialogs = GetCanAcceptDialogList((int)nowNpc.GetCharacterType(), true);//시작가능 대화

                //완료자가 nowPartner(현재 대화 상대)인 퀘스트 받아옴. 제공자는 같을 수도,  다를 수 있음.
                if (canClearqd != null && questManager.ClearQuest(canClearqd.questID, canClearqd.npcID))//있거나 클리어할 수 있다면
                {
                    nowDialogData = questDialogLists[canClearqd.npcID].dialogList[FindDialogIndex(canClearqd)];

                    ss = nowDialogData.clearSentenceInfo;
                    dialogLength = nowDialogData.clearSentenceInfo.Length;
                    sentenceState = 2;//클리어
                }

                else if (isAcceptedQuests.Count > 0) // 진행중인 대화가 있다면?
                {
                    nowDialogData = questDialogLists[(int)nowNpc.GetCharacterType()].dialogList[FindDialogIndex(isAcceptedQuests[0])];

                    ss = nowDialogData.proceedingSentenceInfo;
                    dialogLength = nowDialogData.proceedingSentenceInfo.Length;
                    sentenceState = 1;//진행중
                }
                else if (canStartDialogs.Count > 0)//시작가능 대화가 있다면?
                {
                    //nowDialogData = questDialogLists[nowPartner].dialogList[canStartDialogs[0].questId];
                    nowDialogData = canStartDialogs[0];

                    ss = nowDialogData.acceptSentenceInfo;
                    dialogLength = nowDialogData.acceptSentenceInfo.Length;
                    sentenceState = 0;//수락
                }
                else //아무것도 없다면?
                {
                    canStartDialogs = GetCanAcceptDialogList((int)nowNpc.GetCharacterType(), false);
                    if (canStartDialogs.Count <= 0)
                    {
                        Debug.LogError("StartShowDialog :: nothing else");
                        //dialogUI.SetActive(false);
                        isTalking = false;
                        return;
                    }

                    nowDialogData = canStartDialogs[0];

                    ss = nowDialogData.acceptSentenceInfo;
                    dialogLength = nowDialogData.acceptSentenceInfo.Length;
                }
            }
            //nextButton.onClick.RemoveAllListeners();
            //nextButton.onClick.AddListener(() =>
            //{
            //    UpdateDialog(ss, sentenceState);
            //});
            UpdateDialog(ss, sentenceState);
        }
        public int FindDialogIndex(QuestData questData)
        {
            for (int i = 0; i < questDialogLists[questData.npcID].dialogList.Length; ++i)
            {

                if (questDialogLists[questData.npcID].dialogList[i].questId == questData.questID)
                {

                    return i;
                }
            }
            print("FindDialogIndex return -1");
            return -1;
        }
        private List<DialogData> GetCanAcceptDialogList(int npcID, bool isQuestList)
        {
            List<DialogData> canAcceptDialogs = new List<DialogData>();
            if (isQuestList)
            {
                foreach (var dialogData in questDialogLists[npcID].dialogList)//퀘스트 가진 리스트 중에서 검사
                {
                    if (CanStartTalk(dialogData))
                    {
                        canAcceptDialogs.Add(dialogData);
                    }
                }
            }
            else
            {
                foreach (var dialogData in dailydialogLists[npcID].dialogList)//일반대화 리스트 중에서 검사
                {
                    if (CanStartTalk(dialogData))
                    {
                        canAcceptDialogs.Add(dialogData);
                    }
                }
            }

            return canAcceptDialogs;
        }
        public bool CanStartTalk(DialogData dialogData)
        {
            if (dialogData.haveToHaveAndLikeHouse)//입주 필수 인가?
            {
                if (!nowNpc.IsHaveHouse()) return false;//그렇다면 이 npc는 집을 갖고 있는가?
                //if (buildingManager.GetNPCsHouse(dialogData.subjectCharacterID) == null) return false;//그렇다면 이 npc는 집을 갖고 있는가?
                if (nowNpc.CanGetMyHouse() != BuildingLike.Like) return false;//그렇다면 집에 입주 가능 조건 충족했는가?
            }
            if (dialogData.dontHaveToHaveAndLikeHouse)//미입주 필수 인가?
            {
                if (nowNpc.IsHaveHouse()) return false;// 집이 있으면 false
                //if (buildingManager.GetNPCsHouse(dialogData.subjectCharacterID) != null) return false;// 집이 없는가 ?
            }
            if (dialogData.haveToHaveNPCHouse.Length > 0)
            {
                for (int i = 0; i < dialogData.haveToHaveNPCHouse.Length; i++)
                {
                    if (!npcManager.HaveHouse(dialogData.haveToHaveNPCHouse[i]))
                    {
                        return false;
                    }
                }
            }


            foreach (var item in dialogData.dialogTasks.haveToClearQuest)
            {
                if (!SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                    return false;
            }
            foreach (var item in dialogData.dialogTasks.DonthaveToClearQuest)
            {
                if (SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                    return false;
            }
            foreach (var item in dialogData.dialogTasks.haveToDoingQuest)
            {
                if (!SuperManager.Instance.questmanager.IsQuestAccepted(item.questdata))
                    return false;
            }
            foreach (var item in dialogData.dialogTasks.buildBuildings)
            {
                //빌딩매니저에서 해당 건축물이 어느 집에 설치되어있을 때..
            }
            foreach (var item in dialogData.dialogTasks.haveToEndDialog)
            {
                if (!item.isTalkingOver)
                {
                    return false;
                }
            }
            foreach (var item in dialogData.dialogTasks.DonthaveToEndDialog)
            {
                if (item.isTalkingOver)
                {
                    return false;
                }
            }
            return true;
        }
        public void UpdateDialog(Sentence[] sentences, int sentenceState)
        {
            UpdateDialogText(sentences, sentenceState);
        }

        private void UpdateDialogText(Sentence[] sentences, int sentenceState)
        {
            if (sentences[nowSentenceIdx].eventIdx > 0)
                EventManager.EventAction += EventManager.EventActions[sentences[nowSentenceIdx].eventIdx];

            if (nowOnFab)
            {
                nowOnFab.GetComponent<TextBox>().DestroyTextBox();
            }
            nowOnFab = ObjectPooler.SpawnFromPool("TextBox", gameObject.transform.position);
            nowOnFab.transform.SetParent(npcTalkBubbleTfs[sentences[nowSentenceIdx].characterId]);
            nowOnFab.GetComponent<TextBox>().SetTextbox(sentences[nowSentenceIdx].sentence, npcTalkBubbleTfs[sentences[nowSentenceIdx].characterId], sentences[nowSentenceIdx].textboxType);

            //nowOnFab = Instantiate(textboxFab, npcTalkBubbleTfs[sentences[nowSentenceIdx].characterId]);
            nextButton = nowOnFab.GetComponent<TextBox>().GetNextButton;

            nameText.text = questDialogLists[sentences[nowSentenceIdx++].characterId].charName;


            if (dialogLength == nowSentenceIdx)
            {
                LastDialog(sentenceState);
            }
            else
            {
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() =>
                {
                    UpdateDialog(sentences, sentenceState);
                    //nowOnFab.GetComponent<TextBox>().DestroyTextBox();
                });
            }
            // DOTween.
            //nameText.DOText(sentences[nowSentenceIdx].sentence,1);
            //dialogText.text = sentences[nowSentenceIdx].sentence;
        }

        //마지막 대사일 때 작동
        private void LastDialog(int sentenceState)
        {
            nextButton.onClick.RemoveAllListeners();
            print("Remove_Last");

            nextButton.onClick.AddListener(() =>
            {
                Debug.Log("isTalking false");
                nowOnFab.GetComponent<TextBox>().DestroyTextBox();
                nowOnFab = null;
                nowDialogData.isTalkingOver = true;
                CloseDialog();
                isTalking = false;

                //만약 대화데이터에 퀘스트가 있다면
                if (nowDialogData.questId > -1)
                {
                    //완료 상태 아니라면 강제수락
                    print(sentenceState);
                    switch (sentenceState)
                    {
                        case 0://수락 상태라면?
                            questManager.AcceptQuest(nowDialogData.questId, (int)nowNpc.GetCharacterType());
                            break;
                        default:
                            break;
                    }
                }
            });
            //수락 시 이벤트가 있다면 진행
        }

        private void CloseDialog()
        {
            // dialogUI.SetActive(false);
        }

        public bool IsQuestCleared(int questId, int npcID)//클리어한 대화인지?
        {
            if (questDialogLists[npcID].dialogList[questId].isTalkingOver) return true;
            if (dailydialogLists[npcID].dialogList[questId].isTalkingOver) return true;

            return false;
        }



        #region Data
        //public bool LoadDialogData(int charId, int diaIdx, bool isQuestDialog)//string eventname)
        //{
        //    string filePath = "dimu";
        //    if (isQuestDialog)
        //    {
        //        //Application.persistentDataPath
        //        filePath = Application.dataPath + "/JsonData/" + questDialogLists[charId].dialogList[diaIdx] + ".Json";
        //    }
        //    else
        //    {
        //        filePath = Application.dataPath + "/JsonData/" + dailydialogLists[charId].dialogList[diaIdx] + ".Json";
        //
        //    }
        //    if (File.Exists(filePath))
        //    {
        //        //print(filePath);
        //
        //        string FromJsonData = File.ReadAllText(filePath);
        //        nowDialogData = JsonUtility.FromJson<DialogData>(FromJsonData);
        //        nowSentenceIdx = 0;
        //        Debug.Log(nowSentenceIdx + " dialogIndex 초기화");
        //        return true;
        //    }
        //    else
        //    {
        //        Debug.Log("경로에 파일이 없음");
        //        return false;
        //    }
        //}
        //
        //void CreateJsonFile(string createPath, string fileName, object obj)
        //{
        //    string json = JsonUtility.ToJson(obj);
        //    print(json);
        //    string path = createPath + "/JsonData/" + fileName + ".Json";
        //    File.WriteAllText(path, json);
        //}
        #endregion
    }

    [System.Serializable]
    public class DialogList
    {
        public string charName;
        public DialogData[] dialogList;
    }
}