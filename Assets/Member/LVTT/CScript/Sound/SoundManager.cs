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
        [Range(0f, 1f)]// this to make a slide in Unity
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        public bool loop;
        [HideInInspector]
        public AudioSource source;
    }
    public class SoundManager : MonoBehaviour
    {
        public Sound[] BGM;
        public Sound[] SFX;
        void Awake()
        {
            foreach (Sound s in BGM)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.output;
            }

            foreach (Sound s in SFX)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.output;

            }
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
            if (s == null)
            {
                return;
            }
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

    }

}

