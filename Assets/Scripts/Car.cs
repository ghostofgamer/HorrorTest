using System;
using PlayerContent;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private InteractableObject _interactableObject;

    public event Action Survived;

    private void OnEnable()
    {
        _interactableObject.OnAction += Action;
    }

    private void OnDisable()
    {
        _interactableObject.OnAction -= Action;
    }

    private void Action(PlayerInteraction playerInteraction)
    {
        Survived?.Invoke();
    }
}