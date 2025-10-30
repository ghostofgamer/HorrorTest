using Interfaces;
using UnityEngine;

namespace PlayerContent
{
    public class RaycastVisor : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private float _interactDistance = 5f;
        [SerializeField]private PlayerInteraction _playerInteraction;

        private IInteractable _currentTarget;
        private RaycastHit _lastHit;
        
        void FixedUpdate()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactDistance))
            {
                _lastHit = hit;
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (interactable != _currentTarget)
                    {
                        ClearCurrentTarget();
                        _currentTarget = interactable;
                        _currentTarget.Highlight(true);
                        _playerInteraction.SetCurrentInteractableObject(_currentTarget);
                    }
                    return;
                }
            }
            else
            {
                _lastHit = default;
            }

            ClearCurrentTarget();
        }

        private void ClearCurrentTarget()
        {
            if (_currentTarget != null)
            {
                _currentTarget.Highlight(false);
                _currentTarget = null;
                _playerInteraction.SetCurrentInteractableObject(null);
            }
        }
    }
}