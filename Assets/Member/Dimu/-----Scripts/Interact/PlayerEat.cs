using NSY.Iven;
using UnityEngine;

namespace NSY.Player
{
    public class PlayerEat : MonoBehaviour
    {
        [SerializeField] PlayerController pc;
        [SerializeField] PlayerInteract pi;
        [SerializeField] Animator pa;
        [SerializeField] BaseItemSlot eatItem;

        public bool Eat(BaseItemSlot _eatItem) //player Animator
        {
            eatItem = _eatItem;
            pi.SetIsAnimation(true);
            pa.SetBool("isEating", true);
            pa.GetComponent<PlayerAnimator>().Eat = UpdateEat;
            return true;
        }
        public void UpdateEat()
        {
            pc.playerVital.Tired += eatItem.item.EatAmount;
            eatItem.Amount--;
            eatItem.item.GetCountItems--;
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