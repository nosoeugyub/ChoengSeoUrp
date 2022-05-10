using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NSY.Player
{
    public class PlayerVital : MonoBehaviour
    {
        public int MaxVital = 100;
        public int CurVital;

        public float Tired { get; set; }
        [SerializeField]
        PlayerController playerController;

        [Header("건강")]
        public Image PlayerHealth_image;
        public Image PlayerHealth_icon;
        public Sprite[] playerHealth_sprites;
        //public int Health { get; set; }
        public int MaxHealth;
        public int healthDislatetime = 200;
        private int healthcurrentTime;

        private void Start()
        {
            Tired = 80;
            PlayerHealth_image.fillAmount = Tired;
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
                    Tired-=0.2f;
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
            if (Tired > 70)
                PlayerHealth_icon.sprite = playerHealth_sprites[0];
            else if (Tired > 30)
                PlayerHealth_icon.sprite = playerHealth_sprites[1];
            else
                PlayerHealth_icon.sprite = playerHealth_sprites[2];
            PlayerHealth_image.fillAmount = (float)Tired / MaxHealth;
        }

    }

}
