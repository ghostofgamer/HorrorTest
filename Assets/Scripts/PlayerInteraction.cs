using System;
using Interfaces;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerGameInput _playerGameInput;
    [SerializeField] private Transform _draggablePosition;

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
            // Попробуем вывести имя игрового объекта, если он MonoBehaviour
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
        Debug.Log("Throw");

        if (CurrentDraggable == null)
            return;

        // SoundPlayer.Instance.PlayThrow();
        CurrentDraggable.Throw();
        CurrentDraggable.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10f, ForceMode.Impulse);
        // CurrentDraggable.GetComponent<Rigidbody>().isKinematic = false;
        ClearDraggableObject();
    }
    
    public void SetDraggableObject(Draggable draggable)
    {
        // SoundPlayer.Instance.PlayPickUp();
        CurrentDraggable = draggable;
        draggable.transform.SetParent(_draggablePosition);
        // draggable.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ClearDraggableObject()
    {
        CurrentDraggable.transform.SetParent(null);
        CurrentDraggable = null;
    }
}