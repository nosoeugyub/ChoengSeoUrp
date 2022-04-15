using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Bug
{
    public class BugBehavior : MonoBehaviour
    {
        [SerializeField]
        private string[] arrayBugs;

        [SerializeField]
        private GameObject BugPrefabs;

        [SerializeField]
        private List<BaseGameBug> BaseBug;

        private void Awake()
        {
            BaseBug = new List<BaseGameBug>();

            for (int i = 0; i < arrayBugs.Length; i++)
            {
                GameObject ClneBug = Instantiate(BugPrefabs);
                Bug_fly bugfly = ClneBug.GetComponent<Bug_fly>();
                bugfly.SetUpBugData(arrayBugs[i]);

                BaseBug.Add(bugfly);
            }
        }

        private void Update()
        {
            for (int i = 0; i < BaseBug.Count; i++)
            {
                BaseBug[i].Update();
            }
        }
    }
    }

