using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 아이템 매니저 
/// </summary>
namespace Game.Manager
{
    public class IvenManager : MonoBehaviour
    {
        private void Awake()
        {
            Manager.OnGameStateChange += ManagerOnOnGameStateChanged;
        }

        private void OnDestroy()
        {
            Manager.OnGameStateChange -= ManagerOnOnGameStateChanged;
        }
        private void ManagerOnOnGameStateChanged(GameState state)
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

