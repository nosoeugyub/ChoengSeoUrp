using NSY.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    [SerializeField] PlayerInteract playerInteract;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    //Animation Event
    public void EndAnimation()
    {
        print("EndAnimation");
        playerInteract.isAnimating = false;
    }
}
