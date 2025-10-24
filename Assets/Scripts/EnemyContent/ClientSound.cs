using UnityEngine;

namespace EnemyContent
{
    public class ClientSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _needCoffeeClip;
        [SerializeField] private AudioClip _mutationClip;
        [SerializeField] private AudioClip _screamClip;
        [SerializeField] private AudioClip[] _stopItFrazes;

        [SerializeField] private float _voiceCooldown = 1.5f;

        private float _lastVoiceTime = -10f;

        public void NeedCoffeePlay()
        {
            _audioSource.PlayOneShot(_needCoffeeClip);
        }

        public void MutationPlay()
        {
            _audioSource.PlayOneShot(_mutationClip);
        }

        public void ScreamPlay()
        {
            _audioSource.PlayOneShot(_screamClip);
        }

        public void WhatAreYouDoing()
        {
            if (Time.time - _lastVoiceTime < _voiceCooldown)
                return;

            _lastVoiceTime = Time.time;
            _audioSource.PlayOneShot(_stopItFrazes[Random.Range(0, _stopItFrazes.Length)]);
        }
    }
}