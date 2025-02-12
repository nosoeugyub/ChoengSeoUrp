﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Quest;
using DM.Inven;
using DM.Building;
using DM.Dialog;
using NSY.Iven;
using TT.Sound;
using DM.Event;

namespace NSY.Manager
{ /// <summary>
/// 사용법: 사용하고 싶은 객체에 SuperManager.instance.~~~
/// </summary>
    public class SuperManager : MonoBehaviour
    {
        private static SuperManager _instance;


        [Header("매니저")]
        public UiManager uimanager;
        public QuestManager questmanager;
        public DialogueManager dialogueManager;
        public SpawnManager spawnmanager;
        public InventoryNSY inventoryManager;
        public SoundManager soundManager;
        public UnLockManager unlockmanager;
        public BuildingManager buildingManager;
        public ScreenshotManager scrennshotmangaer;
        public NPCManager npcManager;
        public EnvironmentManager envirmanager;
        public SceneChangeManager scenechagemanage;
        public static SuperManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(SuperManager)) as SuperManager;

                    if (_instance == null)
                    {
                    }
                }
                return _instance;
            }


        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

    }
}

