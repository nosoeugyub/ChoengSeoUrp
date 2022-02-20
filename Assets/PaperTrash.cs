using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        CancelInvoke();
    }
}
