using System.Collections;
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
        hotSpot.y = cursorsprites[idx].height / 2;
        Cursor.SetCursor(cursorsprites[idx], Vector2.zero, CursorMode.Auto);
    }
}
