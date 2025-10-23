using UnityEngine;

namespace UI.Button
{
    public abstract class AbstractButton : MonoBehaviour
    {
        private UnityEngine.UI.Button _button;

        private void Awake()
        {
            _button = GetComponent<UnityEngine.UI.Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public abstract void OnClick();
    }
}