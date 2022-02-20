using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //리스폰의 속성값
    public Transform[] SpawnPos;
    public float CurTime;
    public 

    private void Update()
    {
       
        
            StartCoroutine(SpawnTrash());
        
       
    }

    IEnumerator SpawnTrash()
    {
        yield return new WaitForSeconds(5f);
        if (CurTime >=)
        {
            GameObject PaperTrash = ObjectPooler.SpawnFromPool("PaperTrash", SpawnPos[].transform.position);
        }
            
        
       
       
    }


}
