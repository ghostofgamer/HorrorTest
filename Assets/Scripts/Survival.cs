using EnemyContent;
using PlayerContent;
using UI.Screens;
using UnityEngine;

public class Survival : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private SurvivedScreen _survivedScreen;
    [SerializeField] private DieScreen _dieScreen;

    private Player _player;

    private void OnEnable()
    {
        _car.Survived += OnSurvived;
        _player.Died += OnDie;
    }

    private void OnDisable()
    {
        _car.Survived -= OnSurvived;
        _player.Died -= OnDie;
    }

    public void Init(Player player)
    {
        _player = player;
    }

    private void OnSurvived()
    {
        _survivedScreen.Open();
    }

    private void OnDie()
    {
        _dieScreen.Open();
    }
}