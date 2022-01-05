using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NSY.Manager
{
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }


    public class SoundManager : MonoBehaviour
    {

        public AudioSource[] audioSourceEffect;
        public AudioSource audioSourceBGM;

        public string[] playSoundName;//특정곡

        public Sound[] effectsound;//이펙트 사운드
        public Sound[] BGM;//브금


        private void Start()
        {
            playSoundName = new string[audioSourceEffect.Length];
        }
        public void PlaySE(string _name)
        {
            for (int i = 0; i < effectsound.Length; i++)
            {
                if (_name == effectsound[i].name)
                {
                    for (int j = 0; j < audioSourceEffect.Length; j++)
                    {
                        if (!audioSourceEffect[j].isPlaying)
                        {
                            playSoundName[j] = effectsound[i].name;
                            audioSourceEffect[j].clip = effectsound[i].clip;
                            audioSourceEffect[j].Play();
                            return;
                        }
                    }
                    return;
                }
            }
        }

        public void StopAllSE()
        {
            for (int i = 0; i < audioSourceEffect.Length; i++)
            {
                audioSourceEffect[i].Stop();
            }
        }
        public void StopSE(string _name)
        {
            for (int i = 0; i < audioSourceEffect.Length; i++)
            {
                if (playSoundName[i] == _name)
                {
                    audioSourceEffect[i].Stop();
                    break;
                }
               
            }
        }
    }


}

