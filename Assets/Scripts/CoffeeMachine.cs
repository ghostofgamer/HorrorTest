using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private InteractableObject _interactableObject;

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
        Debug.Log("Наливаю кофе щас");
    }
}