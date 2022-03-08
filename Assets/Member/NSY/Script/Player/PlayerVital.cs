using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Player
{
    public class PlayerVital : MonoBehaviour
    {
      public  int MaxVital = 100;
      public  int CurVital;

        [SerializeField]
        PlayerController playerController;

        [Header("건강")]
        public Image PlayerHealth_imgae;
        public int Health { get; set; }
        public int MaxHealth;
        public int healthDislatetime = 200;
        private int healthcurrentTime;

        private void Start()
        {
            PlayerHealth_imgae.fillAmount = Health;
            Health = 50;
        }

        private void Update()
        {
            StartCoroutine(disVital());
            GaugeUpdate();
        }

        IEnumerator disVital()
        {

            if (Health > 0)
            {
                if (healthcurrentTime <= healthDislatetime)
                {
                    healthcurrentTime++;
                }
                else
                {
                    Health--;
                    healthcurrentTime = 0;
                }
            }


            yield return null;



        }
        void GaugeUpdate()
        {
            PlayerHealth_imgae.fillAmount = (float)Health / MaxHealth;
        }

    }

}
