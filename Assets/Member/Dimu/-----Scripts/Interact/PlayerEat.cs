using UnityEngine;

namespace NSY.Player
{
    public class PlayerEat : MonoBehaviour
    {
        [SerializeField] PlayerController pc;
        [SerializeField] PlayerInteract pi;
        [SerializeField] Animator pa;
        [SerializeField] Item eatItem;

        public void Eat(Item _eatItem) //player Animator
        {
            eatItem = _eatItem;
            pi.SetIsAnimation(true);
            pa.SetBool("isEating", true);
            pa.GetComponent<PlayerAnimator>().Eat = UpdateEat;

        }
        public void UpdateEat()
        {
            pc.playerVital.Tired += eatItem.EatAmount;
            eatItem = null;
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Eat(eatItem);
            }
        }
    }

}
//인벤매니저에 추가
//        if (itemslot.item.OutItemType == OutItemType.Food)
//PlayerEat.Eat(itemslot.item)