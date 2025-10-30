using AttentionContent;
using Enums;
using PlayerContent;
using UnityEngine;

namespace DoorContent
{
    public abstract class Door : MonoBehaviour
    {
        [SerializeField] private KeyType _targetKeyType;
        [SerializeField] private InteractableObject _interactableObject;

        private bool _isLocked = true;

        public abstract void Open();

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
            if (_isLocked)
            {
                if (playerInteraction.CurrentDraggable != null)
                {
                    Key key = playerInteraction.CurrentDraggable.GetComponent<Key>();

                    if (key.KeyType == _targetKeyType)
                    {
                        _isLocked = false;
                        key.gameObject.SetActive(false);
                        playerInteraction.ClearDraggableObject();
                        Open();
                    }
                    else
                    {
                        AttentionHintActivator.ShowHint("Неверный ключ");
                    }
                }
                else
                {
                    AttentionHintActivator.ShowHint("Дверь заперта найди ключ");
                }
            }
            else
            {
                Open();
            }
        }
    }
}