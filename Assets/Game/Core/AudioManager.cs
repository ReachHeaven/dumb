using System;
using UnityEngine;

namespace Game.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip swipeSfx;
        [SerializeField] private AudioClip gameOverSfx;

        private void Start()
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }

        public void PlaySwipe()
        {
            sfxSource.PlayOneShot(swipeSfx);
        }

        public void PlayGameOver()
        {
            sfxSource.PlayOneShot(gameOverSfx);
        }
    }
}
