using UnityEngine;

namespace ItemContent
{
    public class CupCoffee : Item
    {
        [SerializeField] private GameObject _lid;
        [SerializeField] private GameObject _fullnes;

        public bool ProgressCompleted { get; private set; }

        private void Start()
        {
            ChangeProgress();
        }

        public void EnableLid()
        {
            if (_lid != null)
            {
                _lid.SetActive(true);
                ChangeProgress();
            }
            else
            {
                Debug.LogWarning("Lid GameObject is not assigned!");
            }
        }

        public void EnableFullness()
        {
            if (_fullnes != null)
            {
                _fullnes.SetActive(true);
                ChangeProgress();
            }
            else
            {
                Debug.LogWarning("Fullness GameObject is not assigned!");
            }
        }

        private void ChangeProgress()
        {
            ProgressCompleted = _lid != null && _fullnes != null && _lid.activeSelf && _fullnes.activeSelf;
        }
    }
}