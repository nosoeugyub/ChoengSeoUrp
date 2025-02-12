﻿using NSY.Player;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace TT.Sound
{

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;//to add Audio clips
        public AudioMixerGroup output;

        [Range(0f, 3f)]// this to make a slide in Unity
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        public bool loop;
        public bool playInit;
        [HideInInspector]
        public AudioSource source;
    }
    public class SoundManager : MonoBehaviour
    {
        public Sound[] BGM;
        public Sound[] SFX;
        public AudioMixerGroup bgmoutput;
        public AudioMixerGroup sfxoutput;
        public string testSound;
        [SerializeField] AudioMixer audioMixer;

        [SerializeField] PlayerMoveMent playerMoveMent;
        private float volume;

        void Awake()
        {
            foreach (Sound s in BGM)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.output = bgmoutput;
                s.source.outputAudioMixerGroup = s.output;
            }

            foreach (Sound s in SFX)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.output = sfxoutput;
                s.source.outputAudioMixerGroup = s.output;
            }

        }
        private void Start()
        {
            PlayBGM("SummerLine");
        }
        public void PlayBGM(string name)
        {
            Sound s = Array.Find(BGM, sound => sound.name == name);
            if (s == null)
            {
                return;
            }
            s.source.Play();
        }

        public void StopBGM(string name)
        {
            Sound s = Array.Find(BGM, sound => sound.name == name);
            if (s == null)
            {
                return;
            }
            s.source.Stop();
        }  
        

        public void PlaySFX(string name)
        {
            Sound s = Array.Find(SFX, sound => sound.name == name);
            if (s == null || (s.source.isPlaying && !s.playInit)) //playInit false :  다시 재생 안함
            {
                return;
            }
            //playInit true
            s.source.Play();
        }

        public void StopSFX(string name)
        {
            Sound s = Array.Find(SFX, sound => sound.name == name);
            if (s == null)
            {
                return;
            }
            s.source.Stop();
        }

        public void PlayTestSound()
        {
            StopSFX(testSound);
            PlaySFX(testSound);
        }

        public float BGMVolume
        {
            get { audioMixer.GetFloat("BGM", out volume); return volume; }
            set
            {
                if (value <= -30)
                {
                    audioMixer.SetFloat("BGM", -80);
                    return;
                }
                audioMixer.SetFloat("BGM", value);
            }
        }
        public float SFXVolume
        {
            get { audioMixer.GetFloat("SFX", out volume); return volume; }
            set
            {
                if (value <= -30)
                {
                    audioMixer.SetFloat("SFX", -80);
                    return;
                }
                audioMixer.SetFloat("SFX", value);
            }
        }
    }

}

