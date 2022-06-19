using System.Collections;
using UnityEngine;
using NSY.Manager;

namespace NSY.Player
{
    public class PlayerVital : MonoBehaviour
    {
        public int MaxVital = 100;
        public int CurVital;
        public float tired;



        public float Tired
        {
            get
            {
                return tired;
            }
            set
            {
                tired = value;
                if (tired >= 100) tired = 100;
                tiredUi.SetTiredUI(Tired, MaxVital);
              
            }
        }
        [SerializeField]
        PlayerController playerController;

        [Header("건강")]
        //public int Health { get; set; }
        [SerializeField] TiredUI tiredUi;
        public int MaxHealth;
        public int healthDislatetime = 200;
        private int healthcurrentTime;

        private void Start()
        {
            Tired = 80;
        }

        private void Update()
        {
            StartCoroutine(disVital());
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
                    Tired -= 0.2f;
                    healthcurrentTime = 0;
                }
            }
            else
            {
                SuperManager.Instance.envirmanager.TakePictures();
                FindObjectOfType<SceneChangeManager>().LoadSceneString("CreditDemo");
            }
            yield return null;
        }


    }

}
