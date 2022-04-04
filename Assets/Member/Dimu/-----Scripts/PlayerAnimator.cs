using NSY.Player;
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
        animator.SetBool("isMining", false);
        animator.SetBool("isAxing", false);
        animator.SetBool("isEating", false);
        animator.SetBool("isMagnifying", false);
    }
}
