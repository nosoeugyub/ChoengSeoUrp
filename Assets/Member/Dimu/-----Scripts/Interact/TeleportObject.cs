using System.Collections;
using UnityEngine;

public class TeleportObject : AreaInteract
{
    bool isUIOn = false;
    bool isOpen = false;
    Transform playerObj = null;
    [SerializeField] int distfromplayer = 5;
    [SerializeField] ParticleSystem openParticle;

    private void Start()
    {
        gameObject.SetActive(false);
        NSY.Manager.DIalogEventManager.EventActions[(int)EventEnum.MoveToWalPort] += ActiveOn;
    }
    private void ActiveOn()
    {
        gameObject.SetActive(true);
        NSY.Manager.DIalogEventManager.EventActions[(int)EventEnum.MoveToWalPort] -= ActiveOn;
    }
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
            npcManager.OnOfftelePickUI(isUIOn, areaType);
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
                npcManager.OnOfftelePickUI(isUIOn, areaType);
            }
            yield return null;
        }
        yield return null;
    }
    public void ParticleOn()
    {
        openParticle.Play();
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData.AddValue((int)areaType, (int)LocationBehaviorEnum.Interact, PlayerData.locationData, (int)LocationBehaviorEnum.length);
            if (!isOpen)
            {
                ParticleOn();
                isOpen = true;
                npcManager.ButtonInteractable((int)areaType, isOpen);
            }
            //Debug.Log(areaType.ToString());
        }
    }
}