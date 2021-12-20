using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Manager
{
    public enum Sound
    {
        Bgm, //반복재생이나 배경음악
        Effect, //클릭 소리나 퀘스트 완료소리 등등 짧게 한번만 내게
        Count,//갯수세기용 

    }

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource, effectSource, _BackGroundmusic;


        public void PlaySound(AudioClip clip)
        {

        }
    }
}

