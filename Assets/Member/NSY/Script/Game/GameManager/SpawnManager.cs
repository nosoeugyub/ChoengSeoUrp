using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSY.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        //리스폰의 속성값
        public Transform[] SpawnPos;
        public bool[] isSpawn;   
        public float CurTime;
        public float spwanTime = 5f;

        public int maxCount;
        public int Count=0;
        public int SpawnNumber;
        private void Start()
        {
            isSpawn = new bool[SpawnPos.Length];
            for (int i = 0; i < isSpawn.Length; i++)
            {
                isSpawn[i] = false;
            }
        }
        private void Update()
        {
            CurTime += Time.deltaTime;
            if (CurTime >= spwanTime && Count < maxCount)
            {
                StartCoroutine(SpawnTrash(SpawnNumber));
                CurTime = 0;
            }
          
        }

        IEnumerator SpawnTrash(int SpawnNum)
        {

            for ( SpawnNum = 0; SpawnNum < SpawnPos.Length; SpawnNum++)
            {
                GameObject PaperTrash = ObjectPooler.SpawnFromPool("PaperTrash", SpawnPos[SpawnNum].transform.position);

            }
            isSpawn[SpawnNum] = true;
            Count++;
            yield return new WaitForSeconds(0.1f);
          
            





        }


    }

}

