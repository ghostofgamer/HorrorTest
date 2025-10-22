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
        
        private void OnDrawGizmos()
        {
            if (_playerCamera == null) return;

            Vector3 origin = _playerCamera.transform.position;
            Vector3 direction = _playerCamera.transform.forward;

            if (_lastHit.collider != null)
            {
                // Рисуем луч до столкновения
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, _lastHit.point);

                // Точку столкновения
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_lastHit.point, 0.05f);
            }
            else
            {
                // Если ничего не попали — просто рисуем луч фиксированной длины
                Gizmos.color = Color.green;
                Gizmos.DrawLine(origin, origin + direction * _interactDistance);
            }
        }
    }
}