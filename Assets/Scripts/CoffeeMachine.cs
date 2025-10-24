using System;
using System.Collections;
using AttentionContent;
using AudioContent;
using Enums;
using ItemContent;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private InteractableObject _interactableObject;
    [SerializeField] private Transform _cupPosition;
    [SerializeField] private GameObject _effectParticleCofffee;
    [SerializeField] private Collider _colliderCoffeeMachine;

    private CupCoffee _currentCupCoffee;
    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1.5f);

    public event Action CupCoffeeSetted;

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
        if (playerInteraction.CurrentDraggable != null)
        {
            CupCoffee cupCoffee = playerInteraction.CurrentDraggable.GetComponent<CupCoffee>();
            Item lid = playerInteraction.CurrentDraggable.GetComponent<Item>();

            if (cupCoffee != null && !cupCoffee.Completed)
            {
                if (_currentCupCoffee != null)
                {
                    AttentionHintActivator.ShowHint("Кофе машина занята другим стаканом стаканом");
                }
                else
                {
                    playerInteraction.ClearDraggableObject();
                    SetCupCoffee(cupCoffee);
                    CupCoffeeSetted?.Invoke();
                }
            }
            else if (_currentCupCoffee != null && _currentCupCoffee.Fullnes && !_currentCupCoffee.Completed &&
                     lid.ItemType == ItemType.LidCupCoffee)
            {
                playerInteraction.ClearDraggableObject();
                _currentCupCoffee.EnableLid(lid);
            }
            else if (cupCoffee != null && cupCoffee.Completed && cupCoffee.Fullnes)
            {
              
                AttentionHintActivator.ShowHint("У тебя в руках готовое кофе");
            }
            else
            {
                AttentionHintActivator.ShowHint("У тебя в руках не то что требуется");
            }
        }
        else
        {
            if (_currentCupCoffee != null)
            {
                if (!_currentCupCoffee.Fullnes)
                    PourCoffee();
                else if (_currentCupCoffee.Fullnes && _currentCupCoffee.Completed)
                    CompletedCupCoffee(playerInteraction);
                else
                    AttentionHintActivator.ShowHint("Чашка уже полная накрой ее крышкой");
            }
            else
            {
                AttentionHintActivator.ShowHint("Пусто в кофемашине.поставь стаканчик");
            }
        }
    }

    private void SetCupCoffee(CupCoffee cupCoffee)
    {
        _currentCupCoffee = cupCoffee;

        Draggable draggable = _currentCupCoffee.GetComponent<Draggable>();

        if (draggable != null)
        {
            draggable.transform.SetParent(_cupPosition);
            draggable.transform.localPosition = Vector3.zero;
            draggable.transform.localRotation = Quaternion.identity;
        }
    }

    private void PourCoffee()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartPour());
    }

    private IEnumerator StartPour()
    {
        _effectParticleCofffee.SetActive(true);
        _currentCupCoffee.EnableFullness();
        _colliderCoffeeMachine.enabled = false;
        yield return _waitForSeconds;
        _effectParticleCofffee.SetActive(false);
        _colliderCoffeeMachine.enabled = true;
    }

    private void CompletedCupCoffee(PlayerInteraction playerInteraction)
    {
        Draggable draggable = _currentCupCoffee.GetComponent<Draggable>();

        if (draggable != null)
        {
            draggable.Drag(playerInteraction);
            _currentCupCoffee = null;
        }
    }
}