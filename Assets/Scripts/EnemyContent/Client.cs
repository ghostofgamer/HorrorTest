using System.Collections;
using AudioContent;
using Enums;
using PlayerContent;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyContent
{
    public class Client : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private ClientSound _clientSound;

        [SerializeField] private NavMeshObstacle _meshObstacle;
        [SerializeField] private Animator _animator;

        [Header("Speed Settings")] [SerializeField]
        private float _walkSpeed = 1.5f;

        [SerializeField] private float _runSpeed = 4f;
        [SerializeField] private float smooth = 5f;

        [Header("Combat Settings")] [SerializeField]
        private float attackDistance = 1.5f;

        private Transform _cashierPoint;
        private Player _player;
        private float _currentSpeed;
        private float _targetSpeed;
        private ClientState _state;
        private bool _isInitialize = false;

        private void Start()
        {
            SetState(ClientState.MovingToCashier);
        }

        private void Update()
        {
            if (!_isInitialize)
                return;

            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, smooth * Time.deltaTime);
            _animator.SetFloat("Speed", _currentSpeed);

            if (_state == ClientState.Attacking)
            {
                if (_navMeshAgent.isActiveAndEnabled)
                    _navMeshAgent.SetDestination(_player.transform.position);

                if (Vector3.Distance(transform.position, _player.transform.position) <= attackDistance)
                    SetState(ClientState.GameOver);
            }
        }

        public void Init(Player player, Transform cashierPoint)
        {
            this._player = player;
            this._cashierPoint = cashierPoint;
            _isInitialize = true;
        }

        public void SetState(ClientState newState)
        {
            _state = newState;
            StopAllCoroutines();

            switch (newState)
            {
                case ClientState.MovingToCashier:
                    _targetSpeed = _walkSpeed;
                    _navMeshAgent.speed = _walkSpeed;
                    StartCoroutine(MoveToPosition(_cashierPoint.position,
                        () =>
                        {
                            _clientSound.NeedCoffeePlay();
                            SetState(ClientState.WaitingForCoffee);
                        }));
                    break;

                case ClientState.WaitingForCoffee:
                    _targetSpeed = 0f;
                    _navMeshAgent.speed = 0f;
                    break;

                case ClientState.Transforming:
                    StartCoroutine(TransformToMonster());
                    break;

                case ClientState.Attacking:
                    _meshObstacle.enabled = false;
                    _navMeshAgent.enabled = true;
                    _targetSpeed = _runSpeed;
                    _navMeshAgent.speed = _runSpeed;
                    _animator.SetBool("DefaultState", false);
                    _animator.SetBool("MonsterState", true);
                    break;

                case ClientState.GameOver:
                    _navMeshAgent.enabled = false;
                    _meshObstacle.enabled = true;
                    _player.Die();
                    break;
            }
        }

        private IEnumerator TransformToMonster()
        {
            _animator.SetTrigger("Mutation");
            _clientSound.MutationPlay();
            yield return new WaitForSeconds(4f);
            _clientSound.ScreamPlay();
            yield return new WaitForSeconds(2f);
            AudioController.Instance.RunMusic();
            SetState(ClientState.Attacking);
        }

        private IEnumerator MoveToPosition(Vector3 position, System.Action callback)
        {
            _meshObstacle.enabled = false;
            _navMeshAgent.enabled = true;
            yield return null;

            _navMeshAgent.ResetPath();
            _navMeshAgent.SetDestination(position);

            while (_navMeshAgent.pathPending)
                yield return null;

            while (_navMeshAgent.remainingDistance > 1f)
                yield return null;

            _navMeshAgent.enabled = false;
            _meshObstacle.enabled = true;

            callback?.Invoke();
        }

        [ContextMenu("GiveCoffee")]
        public void GiveCoffee()
        {
            if (_state == ClientState.WaitingForCoffee)
                SetState(ClientState.Transforming);
        }
    }
}