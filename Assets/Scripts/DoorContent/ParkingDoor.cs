using AttentionContent;
using UnityEngine;

namespace DoorContent
{
    public class ParkingDoor : Door
    {
        [SerializeField] private float _targetZ = 180f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Transform doorTransform;
        [SerializeField] private bool useLocal = true;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameStateCounter _gameStateCounter;

        private Coroutine _coroutine;

        public override void Open()
        {
            if (!_gameStateCounter.StateRun)
            {
                AttentionHintActivator.ShowHint("Тебе пока рано идти домой");
                return;
            }
            
            _animator.enabled = true;
        }
    }
}