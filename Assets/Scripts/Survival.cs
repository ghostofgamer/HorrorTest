using UI.Screens;
using UnityEngine;

public class Survival : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private SurvivedScreen _survivedScreen;

    private void OnEnable()
    {
        _car.Survived += Survived;
    }

    private void OnDisable()
    {
        _car.Survived -= Survived;
    }

    private void Survived()
    {
        _survivedScreen.Open();
    }

    private void Die()
    {
    }
}