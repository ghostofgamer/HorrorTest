using ItemContent;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private InteractableObject _interactableObject;
    [SerializeField] private Transform _cupPosition;

    private CupCoffee _currentCupCoffee;

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

            if (cupCoffee != null && !cupCoffee.ProgressCompleted)
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
            else
            {
                Debug.Log(" у тебя в руках не стаканчик для кофе");
            }
        }
        else
        {
            Debug.Log("у тебя в руках пусто ");
        }
    }

    private void SetCupCoffee(CupCoffee cupCoffee)
    {
        _currentCupCoffee = cupCoffee;
        
        Draggable draggable = _currentCupCoffee.GetComponent<Draggable>();
        
        if(draggable!=null)
        {
            draggable.transform.SetParent(_cupPosition);
            draggable.transform.localPosition = Vector3.zero;
            draggable.transform.localRotation = Quaternion.identity;
            // draggable.ChangeValue(true);
        }
    }

    private void ClearCupCoffee()
    {
    }
}