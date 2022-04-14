using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSY.Bug
{
    public class BugBehavior : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] arrayBugs;
        [SerializeField]
      //  private GameObject studntPrefab;

        private List<BaseGameBug> BaseBug;

        private void Awake()
        {
            BaseBug = new List<BaseGameBug>();

            for (int i = 0; i < arrayBugs.Length; i++)
            {
                GameObject ClneBug = Instantiate(arrayBugs[i]);
                Bug_fly bugfly = ClneBug.GetComponent<Bug_fly>();

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

