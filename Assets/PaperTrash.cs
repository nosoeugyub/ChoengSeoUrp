using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSY.Manager;
public class PaperTrash : MonoBehaviour
{


    private void OnEnable()
    {
        Invoke(nameof(DeactiveDelay), 30);
    }
    void DeactiveDelay() => gameObject.SetActive(false);
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
     //   SuperManager.Instance.spawnmanager.Count--; //스폰 카운트감소
        CancelInvoke();
        
    }
}
