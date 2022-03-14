
using System.Collections.Generic;

namespace NSY.Player
{
    public class CharacterStat 
    {

        public float BaseValue;
        private readonly List<StatModifier> statModifiers;


        void CharacerStat(float baseValue)//값만 매개변수로 수신하고 목록을 초기화 + 값을 할당
        {
            BaseValue = baseValue;
            //statModifiers = new List<StatModifier>();
        }
    }
}


