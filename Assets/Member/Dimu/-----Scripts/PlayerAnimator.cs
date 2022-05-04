using NSY.Player;
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    [SerializeField] PlayerInteract playerInteract;
    public Action Mine;
    public Action PickUp;
    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }
    //Animation Event
    public void EndAnimation()
    {
        print("EndAnimation");
        playerInteract.isAnimating = false;
        animator.SetBool("isMining", false);
        animator.SetBool("isAxing", false);
        animator.SetBool("isEating", false);
        animator.SetBool("isMagnifying", false);
        animator.SetBool("isPickingUp", false);
    }
    public void MineAnimation()
    {
        //print("Mine");
        Mine();
    }
    public void PickUpAnimation()
    {
        PickUp();
    }
    public void DeleteAni()
    {

    }
}
