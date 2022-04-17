using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AreaType { Sumall, Fallter, WinRing,Sprimmer, Q_1}
public class AreaInteract : MonoBehaviour
{
    [SerializeField] AreaType areaType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerData.AddValue((int)areaType, (int)LocationBehaviorEnum.Interact, PlayerData.locationData, (int)LocationBehaviorEnum.length);
            Debug.Log(areaType.ToString());
        }
    }
}
