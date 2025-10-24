using UnityEngine;

namespace EnemyContent
{
    public class ClientSound : MonoBehaviour
    {
        [SerializeField]private AudioSource _audioSource;
        [SerializeField]private AudioClip _needCoffeeClip;
        [SerializeField]private AudioClip _mutationClip;
        [SerializeField]private AudioClip _screamClip;
        
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
    }
}