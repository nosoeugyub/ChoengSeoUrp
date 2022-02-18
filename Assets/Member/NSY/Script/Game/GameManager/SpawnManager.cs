using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPos;


  IEnumerator SpawnTrash()
    {
        yield return new WaitForSeconds(10f);
        GameObject PaperTrash = ObjectPooling.SpawnFromPool("PaperTrash", SpawnPos[0].transform.position);
       
    }
}
