using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public List<AudioSource> SourcePool;

        private static SoundManager instance;

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SoundManager>();
                }
                return instance;
            }
        }

        public void PlayAudio(AudioClip audioClip, bool loop = false, float pitch = 1.0f, float volume = 1.0f)
        {
            if (audioClip == null)
            {
                return;
            }

            var audioSource = SourcePool.FirstOrDefault(source => !source.isPlaying);

            if (audioSource == null)
            {
                var go = new GameObject("Audio Source");
                audioSource = go.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                SourcePool.Add(audioSource);
            }

            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}