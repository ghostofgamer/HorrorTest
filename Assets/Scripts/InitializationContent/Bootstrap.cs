using System.Collections;
using AttentionContent;
using EnemyContent;
using PlayerContent;
using UI.Screens;
using UnityEngine;

namespace InitializationContent
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private AttentionHintViewer _attentionHintViewer;
        [SerializeField] private Player _player;
        [SerializeField] private Client _client;
        [SerializeField] private Transform _cashierPosition;
        [SerializeField] private Survival _survival;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerSound _playerSound;
        [SerializeField] private GameStateCounter _gameStateCounter;
        [SerializeField] private CoffeeMachine _coffeeMachine;
        [SerializeField] private PlayerRotationTarget _playerRotationTarget;

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            StartCoroutine(InitializeGame());
        }

        private IEnumerator InitializeGame()
        {
            _loadingScreen.Show();

            CursorSwitcher.Hide();

            _loadingScreen.SetProgress(0.1f);
            _client.Init(_player, _cashierPosition);
            yield return new WaitForSeconds(0.1f);

            _loadingScreen.SetProgress(0.4f);
            yield return null;

            AttentionHintActivator.Init(_attentionHintViewer);
            _loadingScreen.SetProgress(0.6f);
            yield return new WaitForSeconds(0.165f);

            _survival.Init(_player);
            _gameStateCounter.Init(_client, _coffeeMachine);
            _loadingScreen.SetProgress(0.9f);
            yield return new WaitForSeconds(0.3f);

            _loadingScreen.SetProgress(1f);
            yield return _loadingScreen.FadeOut();

            _playerController.SwitchController(false);
            _player.gameObject.SetActive(true);
            _playerSound.LastClientPlay();
            yield return new WaitForSeconds(1.5f);
            _client.gameObject.SetActive(true);
            _playerRotationTarget.RotateTowards(_client.transform,1.45F);
            yield return new WaitForSeconds(1.5f);
            _playerController.SwitchController(true);
        }
    }
}