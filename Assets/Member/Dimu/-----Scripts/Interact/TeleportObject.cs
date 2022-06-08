using System.Collections;
using UnityEngine;

public class TeleportObject : Interactable
{
    [SerializeField] AreaType areaType;
    bool isUIOn = false;
    Transform playerObj = null;
   [SerializeField] int distfromplayer = 5;
    public override int CanInteract()
    {
        return (int)CursorType.Mag;
    }

    public void Teleport(Transform transform)
    {
        if (!isUIOn)
        {
            playerObj = transform;
            isUIOn = true;
            FindObjectOfType<NPCManager>().OnOfftelePickUI(isUIOn);
            StartCoroutine(TeleportUIOffCheck());
        }
    }
    IEnumerator TeleportUIOffCheck()
    {
        while (isUIOn)
        {
            if (Vector3.Distance(playerObj.position, transform.position) > distfromplayer)
            {
                isUIOn = false;
                FindObjectOfType<NPCManager>().OnOfftelePickUI(isUIOn);
            }
            yield return null;
        }
        yield return null;
    }
}
