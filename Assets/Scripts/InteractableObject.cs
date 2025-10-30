using System;
using HighlightPlus;
using Interfaces;
using PlayerContent;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private HighlightEffect[] _highlightEffects;

    public event Action<PlayerInteraction> OnAction;

    public event Action<bool> ObjectLooked;

    public void Action(PlayerInteraction playerInteraction)
    {
        OnAction?.Invoke(playerInteraction);
    }

    public void Highlight(bool state)
    {
        if (_highlightEffects.Length > 0)
            foreach (var effect in _highlightEffects)
                effect.enabled = state;
    }
}