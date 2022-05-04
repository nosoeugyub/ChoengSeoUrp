using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Player
{
    public class PlayerVital : MonoBehaviour
    {
        public int MaxVital = 100;
        public int CurVital;

        public int Tired { get; set; }
        [SerializeField]
        PlayerController playerController;

        [Header("건강")]
        public Image PlayerHealth_image;
        //public int Health { get; set; }
        public int MaxHealth;
        public int healthDislatetime = 200;
        private int healthcurrentTime;

        private void Start()
        {
            PlayerHealth_image.fillAmount = Tired;
            Tired = 20;
        }

        private void Update()
        {
            StartCoroutine(disVital());
            //GaugeUpdate();
        }

        IEnumerator disVital()
        {

            if (Tired > 0)
            {
                if (healthcurrentTime <= healthDislatetime)
                {
                    healthcurrentTime++;
                }
                else
                {
                    Tired--;
                    healthcurrentTime = 0;
                    GaugeUpdate();
                }
            }
            else
                FindObjectOfType<SceneChangeManager>().LoadSceneString("EndScene");

            yield return null;



        }
        void GaugeUpdate()
        {
            PlayerHealth_image.fillAmount = (float)Tired / MaxHealth;
        }

    }

}
