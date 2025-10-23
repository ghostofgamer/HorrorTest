using Enums;
using UnityEngine;

namespace ItemContent
{
    public class CupCoffee : Item
    {
        [SerializeField] private GameObject _fullnes;
        [SerializeField] private Transform _lidPosition;

        public bool Fullnes { get; private set; }
        public bool Completed { get; private set; }

        public void EnableLid(Item lidCupCoffee)
        {
            if (!Completed && lidCupCoffee.ItemType == ItemType.LidCupCoffee)
            {
                Draggable draggable = lidCupCoffee.GetComponent<Draggable>();

                if (draggable != null)
                {
                    draggable.ChangeValue(false, true);
                    draggable.transform.SetParent(_lidPosition);
                    draggable.transform.localPosition = Vector3.zero;
                    draggable.transform.localRotation = Quaternion.identity;
                }

                Completed = true;
            }
            else
            {
                Debug.Log("Чашка уже полная и закрытая");
            }
        }

        public void EnableFullness()
        {
            if (_fullnes != null)
            {
                _fullnes.SetActive(true);
                Fullnes = true;
            }
            else
            {
                Debug.LogWarning("Fullness GameObject is not assigned!");
            }
        }
    }
}