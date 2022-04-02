﻿using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static bool isBubbleOn = false;
    float cooldown = 18;
    float tick = 0;
    public void Start()
    {
        InstantiateBubble();
    }

    public void InstantiateBubble()
    {
        MineObject[] mineObjects = FindObjectsOfType<MineObject>();
        int randnum = Random.Range(0, mineObjects.Length);
        mineObjects[randnum].InstantiateBubble();
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
