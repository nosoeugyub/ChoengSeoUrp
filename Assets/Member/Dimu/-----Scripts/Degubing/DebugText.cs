using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    static DebugText inst;
    public float space;
    public GameObject initFab;
    public Vector3 spawnPos;
    List<GameObject> textlist= new List<GameObject>();
    public static DebugText Instance
    {
        get
        {
            if (!inst)
            {
                inst = FindObjectOfType(typeof(DebugText)) as DebugText;

                if (inst == null)
                {
                    Debug.Log("싱글톤 없음 ");
                }
            }
            return inst;
        }


    }
    public void Awake()
    {
        spawnPos = transform.position;
        print(spawnPos);
        inst = this;
    }

    public void SetText(string str)
    {
        spawnPos = transform.position;
        spawnPos.y += space* textlist.Count;
        //print(spawnPos);
        GameObject fab = Instantiate(initFab, spawnPos, Quaternion.identity, transform);
        textlist.Add(fab);
        StartCoroutine(enumerator(fab));
        fab.GetComponent<Text>().text = str;
    }
    IEnumerator enumerator(GameObject go)
    {
        yield return new WaitForSeconds(10);
        spawnPos.y -= space;
        textlist.Remove(go);
        Destroy(go);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SetText("디무는 채고야");
        }
    }
}
