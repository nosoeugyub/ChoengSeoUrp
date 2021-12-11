using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Game.Manager
{
    public enum GameState
    {
        Playerlive,
        PlayQuest,
        PlayBuild,
        PlayerGetItem,
        PlayerDie
    }


    public class Manager : MonoBehaviour
    {
        //하위 ui매니져들 들어갈 예정

        //싱글톤
        public static Manager Instance;
        //첫번째 퀘스트 이벤트
        public  event Action GoVillageQ;






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
                case GameState.PlayQuest:
                    HandlerPlayerQuest();
                    break;
                case GameState.PlayBuild:
                    HandlerPlayerBulld();
                    break;
                case GameState.PlayerDie:
                    HandlerPlayerDie();
                    break;
                case GameState.PlayerGetItem:
                    HandlerPlayerGetItem();
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
        private void HandlerPlayerBulld()
        {

        }
        private void HandlerPlayerQuest()
        {

        }
        private void HandlerPlayerDie()
        {

        }
        private void HandlerPlayerGetItem()
        {

        }
        //퀘스트///////////////////////////////////////////////////////////////////////////////////////
        public void OnFirstQuest()
        {

                if (GoVillageQ != null)
                {

                    GoVillageQ.Invoke();
                }

        }
    }
}









