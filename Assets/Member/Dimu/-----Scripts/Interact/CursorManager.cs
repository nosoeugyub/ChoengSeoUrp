﻿using System.Collections;
using UnityEngine;

enum CursorType { Normal, Axe, PickAxe, Pickup, Talk,Build, Mag,X, }
public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorsprites;
    private Vector2 hotSpot;
    private void Start()
    {
        StartCoroutine(SetCursor((int)CursorType.Normal));
    }
    public IEnumerator SetCursor(int idx)
    {
        yield return new WaitForEndOfFrame();
        hotSpot.x = cursorsprites[idx].width / 2;
        hotSpot.y = 0;
        Cursor.SetCursor(cursorsprites[idx], hotSpot, CursorMode.Auto);
    }
}
