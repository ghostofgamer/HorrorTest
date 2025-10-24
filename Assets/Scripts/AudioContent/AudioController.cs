using System.Collections;
using UnityEngine;

namespace AudioContent
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;

        [Header("BackgroundSounds")] [SerializeField]
        private AudioSource musicSource;

        [SerializeField] private AudioClip _defaultSound;
        [SerializeField] private AudioClip _misticSound;
        [SerializeField] private AudioClip _runSound;
        [SerializeField] private AudioClip _saspensSound;
        [SerializeField] private float _fadeDuration = 1.5f;

        [Header("Heartbeat Sound")] [SerializeField]
        private AudioSource _heartbeatSource;

        [SerializeField] private AudioClip _heartbeatClip;
        [SerializeField] private bool _heartbeatEnabled = false;
        [SerializeField] private float _heartbeatVolume = 0.5f;
        [SerializeField] private float _heartbeatFadeSpeed = 2f;

        private Coroutine _musicFadeCoroutine;
        private float _targetHeartbeatVolume = 0f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (musicSource != null && _defaultSound != null)
            {
                PlayMusic(_defaultSound);
            }

            if (_heartbeatSource != null && _heartbeatClip != null)
            {
                _heartbeatSource.clip = _heartbeatClip;
                _heartbeatSource.loop = true;
                _heartbeatSource.volume = 0f;
                _heartbeatSource.Play();
            }
        }

        private void Update()
        {
            float target = _heartbeatEnabled ? _heartbeatVolume : 0f;
            _heartbeatSource.volume = Mathf.Lerp(_heartbeatSource.volume, target, Time.deltaTime * _heartbeatFadeSpeed);
        }

        public void PlayMusic(AudioClip newMusic)
        {
            if (musicSource == null || newMusic == null)
                return;

            if (_musicFadeCoroutine != null)
                StopCoroutine(_musicFadeCoroutine);

            _musicFadeCoroutine = StartCoroutine(FadeMusic(newMusic));
        }
        
        public void MysticMusic()
        {
            if (_misticSound == null)
            {
                Debug.LogWarning("Mystic clip not assigned in AudioManager!");
                return;
            }
            
            PlayMusic(_misticSound);
            SetHeartbeatActive(true);
            SetHeartbeatIntensity(0.3f); 
            _heartbeatSource.pitch = 1.0f;
        }

        public void RunMusic()
        {
            if (_runSound == null)
            {
                Debug.LogWarning("Run clip not assigned in AudioManager!");
                return;
            }
            musicSource.volume = 0.5f;
            PlayMusic(_runSound);
            
            SetHeartbeatActive(true);
            SetHeartbeatIntensity(1f);
            _heartbeatSource.pitch = 1.3f;
        }

        private IEnumerator FadeMusic(AudioClip newClip)
        {
            float startVolume = musicSource.volume;
            
            for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0f, t / _fadeDuration);
                yield return null;
            }

            musicSource.clip = newClip;
            musicSource.Play();
            
            for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(0f, startVolume, t / _fadeDuration);
                yield return null;
            }

            musicSource.volume = startVolume;
        }
        
        private void SetHeartbeatActive(bool active)
        {
            _heartbeatEnabled = active;
        }

        private void SetHeartbeatIntensity(float intensity)
        {
            _heartbeatVolume = Mathf.Clamp01(intensity);
        }

        public void StopAllAudio()
        {
            musicSource.Stop();
            _heartbeatSource.Stop();
        }
    }
}