﻿using DM.Building;
using DM.Dialog;
using NSY.Manager;
using NSY.Player;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingLike { Like, Unlike_Shape, Unlike_Count, Unlike_Empty, Cant, None, }

namespace DM.NPC
{
    public class HouseNpc : NPC
    {
        [SerializeField] private BuildingBlock myHouse;
        [SerializeField] private Condition[] wantToBuildCondition;
        [SerializeField] private PlayerInteract player;
        [SerializeField] private Transform questMark;
        [SerializeField] private Vector3 questMarkScale;
        [SerializeField] private float speed;
        [SerializeField] private bool isFollowPlayer;
        private DialogueManager dialogueManager;
        private BuildingLike like = BuildingLike.None;

        public BuildingBlock MyHouse { get { return myHouse; } set { myHouse = value; } }

        private void Awake()
        {
            questMarkScale = questMark.localScale;
            dialogueManager = FindObjectOfType<DialogueManager>();
            player = FindObjectOfType<PlayerInteract>();
        }
        private void Start()
        {
            EventManager.EventActions[((int)EventEnum.MoveToMyHome)] += MoveToMyHome;
            EventManager.EventActions[(int)EventEnum.OnFollowPlayer] += OnFollowPlayer;
        }
        private void FixedUpdate()
        {
            if (isFollowPlayer)
                FollowPlayer();
            float dist = Vector3.Distance(player.transform.position, questMark.position) * 0.02f;
            if (dist < 0.5f)
                dist = 0.5f;
            questMark.localScale = questMarkScale * dist;
        }
        public void SetQuestMark(bool ison)
        {
            questMark.gameObject.SetActive(ison);
        }
        public void OnFollowPlayer()
        {
            //현재 대화 상대와 같다면
            if (dialogueManager.GetNowNpc() == this)
            {
                if (player.SetNpc(this))
                {
                    isFollowPlayer = true;
                }
                EventManager.EventAction -= EventManager.EventActions[4];
            }
        }
        public bool IsHaveHouse()
        {
            return myHouse;
        }
        public void FollowPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 4)
            {
                Vector3 dir = player.transform.position - transform.position;
                Vector3 vector3 = new Vector3(transform.position.x + dir.x * Time.deltaTime * speed, transform.position.y, transform.position.z + dir.z * Time.deltaTime * speed);
                MoveTo(vector3);
            }
        }
        public void FindLikeHouse(BuildingBlock buildAreaObject) //해당 건축물에 입주 가능한지.
        {
            if (buildAreaObject.HaveLivingChar())
            {
                like = BuildingLike.Cant;
                SettingBuildingTalk();
                return;
            }
            BuildingLike canGet = GetBuildingLikeable(buildAreaObject); //점수 

            switch (canGet)
            {
                case BuildingLike.Like:
                    SetMyHouse(buildAreaObject, BuildingLike.Like);
                    break;
                default:
                    SetMyHouse(null, canGet);
                    break;
            }
        }
        public BuildingLike CanGetMyHouse()
        {
            return GetBuildingLikeable(myHouse);
        }
        public void SetMyHouse(BuildingBlock block, BuildingLike l)
        {
            like = l;
            if (!SettingBuildingTalk()) return;
            if (block)
            {
                myHouse = block;
                myHouse.SetLivingChar(this);
                print("Find My House");
                MoveToMyHome();
            }
        }
        public BuildingLike GetBuildingLikeable(BuildingBlock buildingBlock) //bool형
        {
            if (!buildingBlock) return BuildingLike.None;
            if (buildingBlock.GetBuildItemList().Count == 0) return BuildingLike.Unlike_Empty;

            List<BuildingItemObj> buildItemList = buildingBlock.GetBuildItemList();

            int[] ints = new int[wantToBuildCondition.Length];

            int failBuildItemCount = 0;

            foreach (BuildingItemObj buildItem in buildItemList)//건축자재 하나씩
            {
                int count = 0;
                for (int i = 0; i < wantToBuildCondition.Length; i++)
                {
                    bool canContinue = true;
                    ints[i]++;

                    foreach (var preferkind in wantToBuildCondition[i].buildItemKind)//설정한 종류들 중 하나 체크
                    {
                        canContinue = false;
                        //희망 조건에 있는 종류와 설치된 자재와 같은지
                        if (preferkind == buildItem.GetAttribute().buildItemKind)
                        {
                            print(preferkind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    //설정된게 있는데 하나도 해당되는게 없으면...
                    if (wantToBuildCondition[i].buildItemKind.Length != 0 && count == wantToBuildCondition[i].buildItemKind.Length)
                    {
                        ++failBuildItemCount; //틀린 자재 개수 증가.

                        //return BuildingLike.Unlike_Shape;
                    }

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildHPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildHPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildHPos.Length != 0 && count == wantToBuildCondition[i].buildHPos.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildVPos)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildVPos)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildVPos.Length != 0 && count == wantToBuildCondition[i].buildVPos.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildSize)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildSize)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildSize.Length != 0 && count == wantToBuildCondition[i].buildSize.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildColor)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildColor)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildColor.Length != 0 && count == wantToBuildCondition[i].buildColor.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildShape)
                    {
                        canContinue = false;
                        if (kind == buildItem.GetAttribute().buildShape)
                        {
                            print(kind.ToString());
                            canContinue = true;
                            break;
                        }
                        count++;
                    }
                    if (wantToBuildCondition[i].buildShape.Length != 0 && count == wantToBuildCondition[i].buildShape.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    count = 0;
                    foreach (var kind in wantToBuildCondition[i].buildThema)
                    {
                        canContinue = false;
                        foreach (var thema in buildItem.GetAttribute().buildThema)
                        {
                            print(kind.ToString());
                            if (kind == thema)
                            {
                                canContinue = true;
                                break;
                            }
                        }
                        if (canContinue) break;
                        count++;
                    }
                    if (wantToBuildCondition[i].buildThema.Length != 0 && count == wantToBuildCondition[i].buildThema.Length)
                        ++failBuildItemCount;
                    //return BuildingLike.Unlike_Shape;

                    if (!canContinue) break;

                    
                }
            }

            for (int i = 0; i < wantToBuildCondition.Length; i++)
            {
                if (wantToBuildCondition[i].buildCount > ints[i])
                {
                    return BuildingLike.Unlike_Count; // false
                }
            }

            for (int i = 0; i < wantToBuildCondition.Length; i++)
            {
                if (wantToBuildCondition[i].buildPerfactCount > ints[i])
                {
                    return BuildingLike.Unlike_Count; // false
                }
            }

            print(failBuildItemCount);
            int halfbuildcount = buildItemList.Count / 2;
            if (halfbuildcount >= failBuildItemCount)
                return BuildingLike.Like;
            else
                return BuildingLike.Unlike_Shape;
        }

        public override int CanInteract()
        {
            return (int)CursorType.Talk;
        }
        public Transform ReturnTF()
        {
            return transform;
        }
        public bool SettingBuildingTalk()
        {
            if (!PlayDialog()) return false;
            if (isFollowPlayer)
            {
                player.SetNpc(null);
                isFollowPlayer = false;
            }
            return true;
        }
        public override void Talk()
        {
            if (isFollowPlayer)
            {
                //player.SetNpc(null);
                //isFollowPlayer = false;
            }
            else
                PlayDialog();
        }
        public bool PlayDialog()
        {
            return SuperManager.Instance.dialogueManager.FirstShowDialog(this, isFollowPlayer, (int)like);
        }
        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }
        public void MoveToMyHome()
        {
            if (myHouse)
            {
                Vector3 vec = myHouse.HouseOwnerTransform.position;
                MoveTo(vec);
            }
            EventManager.EventAction -= EventManager.EventActions[2];
        }
    }
}
