using System.Collections;
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
                    Debug.Log(" Кофе машина занята стаканом");
                }
                else
                {
                    Debug.Log(" То что надо");
                    playerInteraction.ClearDraggableObject();
                    SetCupCoffee(cupCoffee);
                }
            }
            else if (_currentCupCoffee != null && _currentCupCoffee.Fullnes && lid.ItemType == ItemType.LidCupCoffee)
            {
                playerInteraction.ClearDraggableObject();
                _currentCupCoffee.EnableLid(lid);
                CompletedCupCoffee();
            }
            else if (cupCoffee.Completed && cupCoffee.Fullnes)
            {
                Debug.Log(" у тебя в руках готовое кофе");
            }
            else
            {
                Debug.Log(" у тебя в руках не стаканчик для кофе");
            }
        }
        else
        {
            Debug.Log("у тебя в руках пусто ");

            if (_currentCupCoffee != null)
            {
                if (!_currentCupCoffee.Fullnes)
                {
                    PourCoffee();
                }
                else if (_currentCupCoffee.Fullnes && _currentCupCoffee.Completed)
                {
                    Debug.Log("Чашка налита и закрыта уже");
                }
                else
                {
                    Debug.Log("Чашка уже полная накрой ее крышкой");
                }
            }
            else
            {
                Debug.Log("Пусто в кофемашине.поставь стаканчик");
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

    private void CompletedCupCoffee()
    {
        Draggable draggable = _currentCupCoffee.GetComponent<Draggable>();

        if (draggable != null)
        {
            draggable.ChangeValue(true, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CupCoffee cupCoffee))
        {
            if (_currentCupCoffee != null && _currentCupCoffee == cupCoffee)
            {
                _currentCupCoffee = null;
            }
            else
            {
                Debug.Log("НЕ та чашка что была в кофемашине");
            }
        }
    }
}