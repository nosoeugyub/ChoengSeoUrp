﻿       using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace NSY.Manager
{
    public enum GameState
    {
        Playerlive,
        PlayerInteraction,
        PlayerDie
    }


    public class Manager : MonoBehaviour
    {

        //하위 ui매니져들 들어갈 예정

        //싱글톤
        public static Manager Instance;
        //첫번째 다이얼로그 이벤트
        public event Action<int, bool> GoVillageQ;
        //첫번쨰 퀘스트 
        //public event Action<>




        //플레이어 상태
        public GameState State;
        //상태 이벤트 
        public static event Action<GameState> OnGameStateChange;

        private void Awake()
        {
            Instance = this;

        }
        void Start()
        {

            //상태 이벤트
            UpdateState(GameState.Playerlive);
        }
        //플레이어 상태
        public void UpdateState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.Playerlive:
                    HandlerPlayerLeave();
                    break;
                case GameState.PlayerInteraction:
                    HandlerPlayerInteraction();
                    break;
             
                case GameState.PlayerDie:
                    HandlerPlayerDie();
                    break;
             
                default:
                    break;
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            OnGameStateChange?.Invoke(newState);
        }
        private void HandlerPlayerLeave()
        {

        }
        private void  HandlerPlayerInteraction()
        {

        }
        private void HandlerPlayerDie()
        {

        }
       
        //퀘스트///////////////////////////////////////////////////////////////////////////////////////
        public void OnFirstQuest(int id, bool isId)
        {
            if (GoVillageQ != null)
            {
                GoVillageQ.Invoke(id, isId);
            }

        }
    }
     
}









