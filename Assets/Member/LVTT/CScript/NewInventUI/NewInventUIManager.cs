using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventUIManager : MonoBehaviour
{
    [SerializeField] GameObject [] TabUI;
    [SerializeField] ScrollRect TopRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnTabSelect(int TabNum)
    {
        for (int i=0;i<TabUI.Length;i++)
        {
            TabUI[i].SetActive(false);
        }
        TabUI[TabNum].SetActive(true);
        TopRect.content = TabUI[TabNum].GetComponent<RectTransform>();
    }
}
