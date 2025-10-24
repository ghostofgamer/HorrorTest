using UnityEngine;

namespace PlayerContent
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _lastClientClip;
        [SerializeField] private AudioClip _needHelpClip;

        public void LastClientPlay()
        {
            _audioSource.PlayOneShot(_lastClientClip);
        }

        public void NeedHelpPlay()
        {
            _audioSource.PlayOneShot(_needHelpClip);
        }
    }
}