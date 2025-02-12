﻿using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static bool isBubbleOn = false;
    float cooldown = 8;
    float tick = 0;
    public void Start()
    {
        InstantiateBubble();
    }

    public void InstantiateBubble()
    {
        MagnifyObject[] magnifyObjects = FindObjectsOfType<MagnifyObject>();
        if (magnifyObjects.Length == 0) return;
        int randnum = Random.Range(0, magnifyObjects.Length);
        magnifyObjects[randnum].InstantiateBubble();
        isBubbleOn = true;
        tick = 0;
       
    }
    public static void CheckBubble()
    {
        isBubbleOn = false;
       

    }
    private void Update()
    {
        if (!isBubbleOn)
        {
            tick += Time.deltaTime;
            if (tick > cooldown)
            {
                InstantiateBubble();
            }
        }
    }
}
