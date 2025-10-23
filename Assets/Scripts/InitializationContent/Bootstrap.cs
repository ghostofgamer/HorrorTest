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
            _loadingScreen.SetProgress(0.9f);
            yield return new WaitForSeconds(0.3f);

            _loadingScreen.SetProgress(1f);
            yield return _loadingScreen.FadeOut();
        }
    }
}