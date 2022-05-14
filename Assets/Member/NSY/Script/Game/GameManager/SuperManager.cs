using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DM.Quest;
using DM.Inven;
using DM.Building;
using DM.Dialog;
using NSY.Iven;

namespace NSY.Manager
{ /// <summary>
/// 사용법: 사용하고 싶은 객체에 SuperManager.instance.~~~
/// </summary>
    public class SuperManager : MonoBehaviour
    {
        private static SuperManager _instance;


        [Header("매니저")]
        public UiManager uimanager;
        
       // public TutorialManager tutorialManager;
        public QuestManager questmanager;
        public DialogueManager dialogueManager;
        public SpawnManager spawnmanager;
        public InventoryNSY inventoryManager;


        public static SuperManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(SuperManager)) as SuperManager;

                    if (_instance == null)
                    {
                        Debug.Log("싱글톤 없음 ");
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

