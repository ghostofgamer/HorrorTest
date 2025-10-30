using System;
using Interfaces;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PlayerContent
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerGameInput _playerGameInput;
        [SerializeField] private Transform _draggablePosition;
        [SerializeField] private float _force = 10f;

        private IInteractable _currentInteractable;

        public event Action<IInteractable> CurrentDraggerChanger;

        public Transform DraggablePosition => _draggablePosition;
        public Draggable CurrentDraggable { get; private set; }

        private void OnEnable()
        {
            _playerGameInput.ActionEvent += Action;
            _playerGameInput.ThrowEvent += ThrowItem;
        }

        private void OnDisable()
        {
            _playerGameInput.ActionEvent -= Action;
            _playerGameInput.ThrowEvent -= ThrowItem;
        }

        public void SetCurrentInteractableObject(IInteractable iInteractable)
        {
            _currentInteractable = iInteractable;
            CurrentDraggerChanger?.Invoke(_currentInteractable);

            if (_currentInteractable != null)
            {
                if (_currentInteractable is MonoBehaviour mb)
                {
                    Debug.Log(
                        $"[Interactor] Текущий интерактивный объект: {mb.name} ({_currentInteractable.GetType().Name})");
                }
                else
                {
                    Debug.Log(
                        $"[Interactor] Текущий интерактивный объект (без MonoBehaviour): {_currentInteractable.GetType().Name}");
                }
            }
            else
            {
                Debug.Log("[Interactor] Интерактивный объект сброшен (null)");
            }
        }

        private void Action()
        {
            if (_currentInteractable != null)
                _currentInteractable.Action(this);
        }

        private void ThrowItem()
        {
            if (CurrentDraggable == null)
                return;

            CurrentDraggable.Throw();
            CurrentDraggable.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _force, ForceMode.Impulse);
            ClearDraggableObject();
        }

        public void SetDraggableObject(Draggable draggable)
        {
            CurrentDraggable = draggable;
            draggable.transform.SetParent(_draggablePosition);
        }

        public void ClearDraggableObject()
        {
            CurrentDraggable.transform.SetParent(null);
            CurrentDraggable = null;
        }
    }
}