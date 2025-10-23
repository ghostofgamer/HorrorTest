using System;
using DG.Tweening;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private InteractableObject _interactableObject;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Rigidbody _rigidbody;

    public event Action DraggablePicked;

    public event Action DraggableThrowed;

    public event Action PutOnShelfCompleting;

    public bool InHands;

    private void OnEnable()
    {
        _interactableObject.OnAction += Drag;
    }

    private void OnDisable()
    {
        _interactableObject.OnAction -= Drag;
    }

    public virtual void Drag(PlayerInteraction playerInteraction)
    {
        if (playerInteraction.CurrentDraggable != null)
        {
            Debug.Log("Return");
        }
        else
        {
            ChangeValue(false, true);
            InHands = true;
            _parentObject.transform.parent = playerInteraction.DraggablePosition;
            _parentObject.DOLocalMove(Vector3.zero, 0.15f).SetEase(Ease.InOutQuad);
            _parentObject.DOLocalRotate(Vector3.zero, 0.15f).SetEase(Ease.InOutQuad);

            playerInteraction.SetDraggableObject(this);
            DraggablePicked?.Invoke();
        }
    }

    public void Throw()
    {
        InHands = false;
        DraggableThrowed?.Invoke();
        ChangeValue(true, false);
    }

    public void ChangeValue(bool colliderValue, bool rigidbodyValue)
    {
        foreach (var collider in _colliders)
            collider.enabled = colliderValue;

        _rigidbody.isKinematic = rigidbodyValue;
    }
}