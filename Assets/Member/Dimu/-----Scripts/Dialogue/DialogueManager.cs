using DM.NPC;
using DM.Quest;
using NSY.Manager;
using NSY.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Character
{ CheongSeo, Ejang, Walrus, Hen, Bee, Rabbit, Deer, Milkcow, Sheep, Length }
//청서 곰 닭 바코 벌 토끼 사슴 젖소 양
public enum LanguageType
{ Korean, English, Vietnamese, }
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
        public Text dialogText;
        public Text nameText;

        PlayerInput.InputEvent dialogdelegate;
        PlayerInput.InputEvent savedelegate;

        public List<DialogList> activeQuestDialogLists; //퀘스트 있는 대화
        public List<DialogList> activeDailydialogLists; //퀘스트 없는 대화
        public List<DialogList> activeBuildDialogLists; //건축 피드백 대화

        [Header("DialogInfos_kor")]
        public List<DialogList> questDialogLists; //퀘스트 있는 대화
        public List<DialogList> dailydialogLists; //퀘스트 없는 대화
        public List<DialogList> buildDialogLists; //건축 피드백 대화

        [Header("DialogInfos_eng")]
        public List<DialogList> questDialogLists_eng; //퀘스트 있는 대화
        public List<DialogList> dailydialogLists_eng; //퀘스트 없는 대화
        public List<DialogList> buildDialogLists_eng; //건축 피드백 대화

        [Header("DialogInfos_viet")]
        public List<DialogList> questDialogLists_viet; //퀘스트 있는 대화
        public List<DialogList> dailydialogLists_viet; //퀘스트 없는 대화
        public List<DialogList> buildDialogLists_viet; //건축 피드백 대화

        [Header("InstanciatePrefab")]
        public GameObject textboxFab;//대화창 프리펩 //쪽지로 변하는것임!!

        GameObject nowOnFab;
        QuestData canClearqd;
        HouseNpc nowNpc;
        [SerializeField] bool isTalking = false;
        float times = 0;
        [SerializeField] float interactdelaytime;
        bool isenddelay = true;

        QuestManager questManager;
        NPCManager npcManager;
        CutScene cutSceneManager;
        [SerializeField] LanguageType nowLanguageType;

        [Header("AlphaCanvases")]
        [SerializeField] CanvasGroup[] alphaCanvases;
        [SerializeField] Image raycastBlockImg;

        Coroutine[] nowCor;
        Coroutine coroutine;

        public bool IsTalking
        {
            get { return isTalking; }
            set
            {
                isTalking = value;
                if (!isTalking)
                {
                    for (int i = 0; i < alphaCanvases.Length; ++i)
                    {
                        if (nowCor[i] != null)
                            StopCoroutine(nowCor[i]);
                        nowCor[i] = StartCoroutine(CanvasAlphaUp(alphaCanvases[i], true, 10));
                    }
                }
            }
        }
        private void Awake()
        {
            nowCor = new Coroutine[alphaCanvases.Length];
            questManager = SuperManager.Instance.questmanager;
            npcManager = FindObjectOfType<NPCManager>();
            cutSceneManager = FindObjectOfType<CutScene>();
        }
        void Start()
        {
            DIalogEventManager.EventActions[((int)EventEnum.Test)] = Test;
            DIalogEventManager.EventActions[(int)EventEnum.StartTalk] += StartNewDialog;
            SetDialogSet(nowLanguageType);
            StartCoroutine(firstDialog());
        }
        public void Update()
        {
            if (IsTalking && Vector3.Distance(GetNowNpc().transform.position, npcManager.NpcTfs[0].Npctf.transform.position) > 10)
            {
                CancleDIalog();
                DebugText.Instance.SetText(string.Format("대화 중인 상대와 거리가 멀어져 대화가 취소되었습니다."));
            }
        }
        IEnumerator CanvasAlphaUp(CanvasGroup canvasGroup, bool isUp, float speed)
        {
            if (isUp)
            {
                while (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += Time.deltaTime * speed;
                    yield return null;
                }
                raycastBlockImg.raycastTarget = false;
            }
            else
            {
                raycastBlockImg.raycastTarget = true;
                while (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= Time.deltaTime * speed;
                    yield return null;
                }
            }
        }
        IEnumerator firstDialog()
        {
            yield return new WaitForSeconds(0.1f);
            savedelegate = PlayerInput.OnPressFDown;

            FirstShowDialog(npcManager.NpcTfs[0].Npctf, false, -1);
        }
        private void StartNewDialog()
        {
            nowDialogData.isTalkingOver = true;
            IsTalking = false;
            FirstShowDialog(npcManager.NpcTfs[0].Npctf, false, -1);
            DIalogEventManager.EventAction -= DIalogEventManager.EventActions[(int)EventEnum.StartTalk];
        }
        public void SetDialogSet(LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.Korean:
                    activeQuestDialogLists = questDialogLists;
                    activeDailydialogLists = dailydialogLists;
                    activeBuildDialogLists = buildDialogLists;
                    break;
                case LanguageType.English:
                    activeQuestDialogLists = questDialogLists_eng;
                    activeDailydialogLists = dailydialogLists_eng;
                    activeBuildDialogLists = buildDialogLists_eng;
                    break;
                case LanguageType.Vietnamese:
                    activeQuestDialogLists = questDialogLists_viet;
                    activeDailydialogLists = dailydialogLists_viet;
                    activeBuildDialogLists = buildDialogLists_viet;
                    break;
                default:
                    break;
            }
        }
        public HouseNpc GetNowNpc()
        {
            return nowNpc;
        }
        public void Test()
        {
            print("event test");
            times += Time.deltaTime;
            if (times > 3) { DIalogEventManager.EventAction -= DIalogEventManager.EventActions[1]; }
        }
        public void ResetDelay()
        {
            StopCoroutine(coroutine);
            isenddelay = true;
        }
        public bool FirstShowDialog(HouseNpc npc, bool isFollowPlayer, int isLike) //첫 상호작용 시 호출. 어떤 대화를 호출할지 결정
        {
            if (isenddelay == false) return false;
            if (isTalking)
            {
                DebugText.Instance.SetText(string.Format("{0}님과 대화중입니다!", nowNpc.name));
                return false;
            }
            IsTalking = true;
            nowNpc = npc;
            npcManager.NowInteractNPCIndex = (int)nowNpc.GetCharacterType();
            npcManager.PlayNPCDialogSound(npcManager.NowInteractNPCIndex);
            partnerTf = npc.transform; //이 변수 삭제하고 npcmanager 쓰자
            nowSentenceIdx = 0;

            PlayerData.AddValue((int)npc.GetCharacterType(), (int)NpcBehaviorEnum.Interact, PlayerData.npcData, (int)NpcBehaviorEnum.length);

            StartShowDialog(isFollowPlayer, isLike); //파트너와 진행해야 하는 순서의 대화를 진행
            return true;
        }

        public void StartShowDialog(bool isFollowPlayer, int isLike)
        {
            Sentence[] ss = null;//대화뭉치를 담을 변수
            int sentenceState = -1;//대화뭉치 타입(수락0, 진행중1, 완료2)

            if (isFollowPlayer)
            {
                if (isLike == (int)BuildingLike.None) return;

                nowDialogData = activeBuildDialogLists[(int)nowNpc.GetCharacterType()].dialogList[isLike];
                ss = nowDialogData.acceptSentenceInfo;
                dialogLength = nowDialogData.acceptSentenceInfo.Length;
                sentenceState = -1;
            }
            else
            {
                canClearqd = questManager.ReturnCanClearQuestRequireNpc((int)nowNpc.GetCharacterType());

                List<QuestData> isAcceptedQuests = questManager.GetIsAcceptedQuestList((int)nowNpc.GetCharacterType());//진행중인 대화 
                List<DialogData> canStartDialogs = GetCanAcceptDialogList((int)nowNpc.GetCharacterType(), true);//시작가능 대화
                DialogData canStartDialog = null;

                foreach (DialogData data in canStartDialogs)
                {
                    if (data.questId < 0)
                    {
                        //Debug.Log("canStartDialog = data");
                        canStartDialog = data;
                        break;
                    }
                    else
                    {
                        if (!questManager.IsQuestAccepted(questManager.nowQuestLists[(int)nowNpc.GetCharacterType()].questList[data.questId]))
                        {
                            //Debug.Log("canStartDialog = data");
                            canStartDialog = data;
                            break;
                        }
                    }
                }


                //완료자가 nowPartner(현재 대화 상대)인 퀘스트 받아옴. 제공자는 같을 수도,  다를 수 있음.
                if (canClearqd != null && questManager.ClearQuest(canClearqd.questID, canClearqd.npcID))//있거나 클리어할 수 있다면
                {
                    nowDialogData = activeQuestDialogLists[canClearqd.npcID].dialogList[FindDialogIndex(canClearqd)];

                    ss = nowDialogData.clearSentenceInfo;
                    dialogLength = nowDialogData.clearSentenceInfo.Length;
                    sentenceState = 2;//클리어
                }
                //하 슈발 코드 개기네 일단 대화 가능한 
                else if (canStartDialog)//시작가능 대화가 있다면?
                {
                    //nowDialogData = questDialogLists[nowPartner].dialogList[canStartDialogs[0].questId];

                    nowDialogData = canStartDialog;

                    ss = nowDialogData.acceptSentenceInfo;
                    dialogLength = nowDialogData.acceptSentenceInfo.Length;
                    sentenceState = 0;//수락
                }
                else if (isAcceptedQuests.Count > 0 && CanAccept(isAcceptedQuests)) // 진행중인 대화가 있다면?
                {
                    nowDialogData = activeQuestDialogLists[(int)nowNpc.GetCharacterType()].dialogList[FindDialogIndex(isAcceptedQuests[0])];

                    ss = nowDialogData.proceedingSentenceInfo;
                    dialogLength = nowDialogData.proceedingSentenceInfo.Length;
                    sentenceState = 1;//진행중
                }

                else //아무것도 없다면?
                {
                    canStartDialogs = GetCanAcceptDialogList((int)nowNpc.GetCharacterType(), false);
                    if (canStartDialogs.Count <= 0)
                    {
                        //DebugText.Instance.SetText(string.Format("진행할 대화가 없습니다."));

                        IsTalking = false;
                        return;
                    }
                    nowDialogData = canStartDialogs[0];

                    ss = nowDialogData.acceptSentenceInfo;
                    dialogLength = nowDialogData.acceptSentenceInfo.Length;
                }
            }

            for (int i = 0; i < alphaCanvases.Length; ++i)
            {
                if (nowCor[i] != null)
                    StopCoroutine(nowCor[i]);
                nowCor[i] = StartCoroutine(CanvasAlphaUp(alphaCanvases[i], false, 10));
            }

            UpdateDialogText(ss, sentenceState);
        }

        private bool CanAccept(List<QuestData> isAcceptedQuests)
        {
            if (isAcceptedQuests[0].tasks.npcs.Length > 0)//npc 항목 있고
            {
                if (isAcceptedQuests[0].tasks.npcs[0].behaviorType != 1) //집 퀘스트 아니면 
                    return true;
                else //집퀘면
                    return false;
            }
            else //없다면
            {
                return true;
            }
        }

        public int FindDialogIndex(QuestData questData)
        {
            for (int i = 0; i < activeQuestDialogLists[questData.npcID].dialogList.Length; ++i)
            {

                if (activeQuestDialogLists[questData.npcID].dialogList[i].questId == questData.questID)
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
                foreach (var dialogData in activeQuestDialogLists[npcID].dialogList)//퀘스트 가진 리스트 중에서 검사
                {
                    if (CanStartTalk(dialogData, nowNpc))
                    {
                        canAcceptDialogs.Add(dialogData);
                    }
                }
            }
            else
            {
                foreach (var dialogData in activeDailydialogLists[npcID].dialogList)//일반대화 리스트 중에서 검사
                {
                    if (CanStartTalk(dialogData, nowNpc))
                    {
                        canAcceptDialogs.Add(dialogData);
                    }
                }
            }

            return canAcceptDialogs;
        }
        private List<DialogData> GetCanAcceptDialogList(int npcID)
        {
            List<DialogData> canAcceptDialogs = new List<DialogData>();
            foreach (DialogData dialogData in activeQuestDialogLists[npcID].dialogList)//퀘스트 가진 리스트 중에서 검사
            {
                if (dialogData.isTalkingOver) continue;
                if (CanStartTalk(dialogData, npcManager.NpcTfs[npcID].Npctf))
                {
                    canAcceptDialogs.Add(dialogData);
                }
            }

            return canAcceptDialogs;
        }
        public bool CanStartTalk(DialogData dialogData, HouseNpc npc)
        {
            // if (dialogData.isTalkingOver) return false;

            if (dialogData.haveToHaveAndLikeHouse)//입주 필수 인가?
            {
                if (!npc.IsHaveHouse()) return false;//그렇다면 이 npc는 집을 갖고 있는가?
                                                     //if (buildingManager.GetNPCsHouse(dialogData.subjectCharacterID) == null) return false;//그렇다면 이 npc는 집을 갖고 있는가?
                                                     //if (nowNpc.CanGetMyHouse() != BuildingLike.Like) return false;//그렇다면 집에 입주 가능 조건 충족했는가?
                                                     //print("입주 필수인 대화입니다.");
            }
            if (dialogData.dontHaveToHaveAndLikeHouse)//미입주 필수 인가?
            {
                if (npc.IsHaveHouse()) return false;// 집이 있으면 false
                                                    //if (buildingManager.GetNPCsHouse(dialogData.subjectCharacterID) != null) return false;// 집이 없는가 ?
            }
            if (dialogData.haveToHaveNPCHouse.Length > 0)
            {
                for (int i = 0; i < dialogData.haveToHaveNPCHouse.Length; i++)
                {
                    if (!npcManager.HaveHouse(dialogData.haveToHaveNPCHouse[i]))
                    {
                        //print("필요 NPC가 집이 없습니다.");
                        return false;
                    }
                }
            }


            foreach (var item in dialogData.dialogTasks.haveToClearQuest)
            {
                //print("haveToClearQuest: " + item.questdata.questName);
                if (!SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                    return false;
            }
            foreach (var item in dialogData.dialogTasks.DonthaveToClearQuest)
            {
                //print("DonthaveToClearQuest: " + item.questdata.questName);
                if (SuperManager.Instance.questmanager.IsQuestCleared(item.questdata))
                    return false;
            }
            foreach (var item in dialogData.dialogTasks.haveToDoingQuest)
            {
                //print("DonthaveToClearQuest: " + item.questdata.questName);
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
                    //print("haveToEndDialog: 끝난 대화가 아님 false " + item.name);
                    return false;
                }
            }
            foreach (var item in dialogData.dialogTasks.DonthaveToEndDialog)
            {
                if (item.isTalkingOver)
                {
                    //print("haveToEndDialog: 끝난대화임 false " + item.name);
                    return false;
                }
            }
            return true;
        }

        private void UpdateDialogText(Sentence[] sentences, int sentenceState)
        {
            if (isenddelay == false) return;


            if (sentences.Length == 0)
            {
                LastDialogNextEvent(sentences, sentenceState);
            }


            if (sentences[nowSentenceIdx].eventIdx > 0)
                DIalogEventManager.EventAction += DIalogEventManager.EventActions[sentences[nowSentenceIdx].eventIdx];

            if (nowSentenceIdx > 0 && sentences[nowSentenceIdx - 1].backeventIdx > 0)
            {
                DIalogEventManager.EventAction += DIalogEventManager.BackEventActions[sentences[nowSentenceIdx - 1].backeventIdx];
            }

            if (nowOnFab)
            {
                nowOnFab.GetComponent<TextBox>().DestroyTextBox();
            }

            Sentence nowSentences = sentences[nowSentenceIdx];

            nowOnFab = ObjectPooler.SpawnFromPool("TextBox", npcTalkBubbleTfs[nowSentences.characterId].transform.position);
            //nowOnFab.transform.SetParent(npcTalkBubbleTfs[nowSentences.characterId]);
            nowOnFab.GetComponent<TextBox>().SetTextbox(nowSentences.sentence, npcTalkBubbleTfs[nowSentences.characterId], nowSentences.textboxType, nowSentences.isLeft);

            nameText.text = activeQuestDialogLists[sentences[nowSentenceIdx++].characterId].charName;
            coroutine =  StartCoroutine(DelayUpdateBool(sentences, sentenceState));

            if (dialogLength <= nowSentenceIdx)
            {
                LastDialog(sentences, sentenceState);
            }
            else
            {
                dialogdelegate = (() =>
                {
                    //if (sentences[nowSentenceIdx - 1].backeventIdx > 0)
                    //{
                    //    DIalogEventManager.EventAction += DIalogEventManager.BackEventActions[sentences[nowSentenceIdx - 1].backeventIdx];
                    //    Debug.Log(nowSentenceIdx + " back 이벤트실행!");
                    //}


                    UpdateDialogText(sentences, sentenceState);
                });
                PlayerInput.OnPressFDown = dialogdelegate;
            }
        }
        IEnumerator DelayUpdateBool(Sentence[] sentences, int sentenceState)
        {
            isenddelay = false;
            yield return new WaitForSeconds(interactdelaytime);
            isenddelay = true;
        }
        //마지막 대사일 때 작동
        private void LastDialog(Sentence[] sentences, int sentenceState)
        {
            dialogdelegate = (() =>
            {
                LastDialogNextEvent(sentences, sentenceState);

            });
            PlayerInput.OnPressFDown = dialogdelegate;
        }

        private void LastDialogNextEvent(Sentence[] sentences, int sentenceState)
        {
            if (isenddelay == false) return;
            if (nowOnFab)
            {
                nowOnFab.GetComponent<TextBox>().DestroyTextBox();
                nowOnFab = null;
            }
            dialogdelegate = null;

            IsTalking = false;
            nowDialogData.isTalkingOver = true;

            coroutine = StartCoroutine(DelayUpdateBool(sentences, sentenceState));

            //만약 대화데이터에 퀘스트가 있다면
            if (nowDialogData.questId > -1)
            {
                //완료 상태 아니라면 강제수락
                switch (sentenceState)
                {
                    case 0://수락 상태라면?
                        if (nowDialogData.acceptQuestItems.Length > 0)
                        {
                            foreach (DialogData.QuestRewards item in nowDialogData.acceptQuestItems)//아이템추가해야함.
                            {
                                if (item.rewardType == RewardType.Item)
                                {
                                    if (SuperManager.Instance.inventoryManager.CanAddInven(item.itemType))
                                        SuperManager.Instance.inventoryManager.AddItem(item.itemType, item.getCount, true);
                                    else
                                    {
                                        nowDialogData.isTalkingOver = false;
                                        break;
                                    }
                                }
                                else if (item.rewardType == RewardType.Event)
                                {
                                    DIalogEventManager.EventAction += DIalogEventManager.EventActions[item.getCount];
                                }
                                //개수만큼 더하게 해야함
                            }
                        }
                        questManager.AcceptQuest(nowDialogData.questId, (int)nowNpc.GetCharacterType());

                        break;
                    case 2:
                        if (nowDialogData.cuttype != CutType.None)
                        {
                            cutSceneManager.PrintImage((int)nowDialogData.cuttype);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {

                if (nowDialogData.cuttype != CutType.None)
                {
                    cutSceneManager.PrintImage((int)nowDialogData.cuttype);
                }
            }



            if (sentences[nowSentenceIdx - 1].backeventIdx > 0)
            {
                DIalogEventManager.EventAction += DIalogEventManager.BackEventActions[sentences[nowSentenceIdx - 1].backeventIdx];
                Debug.Log(nowSentenceIdx + " last back 이벤트실행!");
            }

            PlayerInput.OnPressFDown = savedelegate;
            nowNpc = null;
            UpdateNpcsQuestMark();
        }
        public void CancleDIalog()
        {
            nowOnFab.GetComponent<TextBox>().DestroyTextBox();
            nowOnFab = null;
            dialogdelegate = null;
            IsTalking = false;

            PlayerInput.OnPressFDown = savedelegate;
            nowNpc = null;
            UpdateNpcsQuestMark();
        }
        public void UpdateNpcsQuestMark()
        {
            QuestData qd = questManager.ReturnCanClearQuestRequireNpc(0); //클리어가능한 퀘스트 0번 인덱스

            // 튜토리얼
            if (qd && qd.npcID == 0)
            {
                if (qd.questID > -1)
                {
                    if (questManager.ClearQuest(qd.questID, qd.npcID))
                    {
                        if (qd.questID < 7)
                            questManager.AcceptQuest(qd.questID + 1, qd.npcID);
                        else
                        {
                            FirstShowDialog(npcManager.NpcTfs[0].Npctf, false, -1);
                        }
                    }
                }
            }

            for (int i = 1; i < npcManager.NpcTfs.Length; i++)
            {
                qd = questManager.ReturnCanClearQuestRequireNpc(i); //클리어가능한 퀘스트 0번 인덱스
                //List<QuestData> isAcceptedQuests = questManager.GetIsAcceptedQuestList(i);//진행중인 대화 
                List<DialogData> canStartDialogs = GetCanAcceptDialogList(i);//시작가능 대화

                if (qd != null)
                {
                    npcManager.NpcTfs[i].Npctf.SetQuestMark(DialogMarkType.CanClear);//, true);
                }
                else if (canStartDialogs.Count > 0)
                {
                    npcManager.NpcTfs[i].Npctf.SetQuestMark(DialogMarkType.CanStart);//, true);

                }
                else
                {
                    npcManager.NpcTfs[i].Npctf.SetQuestMark(DialogMarkType.None);//, false);
                }


            }
        }

        public bool IsQuestCleared(int questId, int npcID)//클리어한 대화인지?
        {
            if (activeQuestDialogLists[npcID].dialogList[questId].isTalkingOver) return true;
            if (activeDailydialogLists[npcID].dialogList[questId].isTalkingOver) return true;

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