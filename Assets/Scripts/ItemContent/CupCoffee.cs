using Enums;
using UnityEngine;

namespace ItemContent
{
    public class CupCoffee : Item
    {
        [SerializeField] private GameObject _lid;
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
                    Debug.Log("фываываываыва  1");
                    draggable.ChangeValue(false, true);
                    draggable.transform.SetParent(_lidPosition);
                    Debug.Log("фываываываыва  3 " + draggable.gameObject.name + _lidPosition.gameObject.name);
                    draggable.transform.localPosition = Vector3.zero;
                    draggable.transform.localRotation = Quaternion.identity;
                }

                Completed = true;
            }
            else
            {
                Debug.Log("Чашка уже полная и закрытая");
            }


            /*if (_lid != null)
            {
                Destroy(lidCupCoffee);
                _lid.SetActive(true);
                Completed = true;
            }
            else
            {
                Debug.LogWarning("Lid GameObject is not assigned!");
            }*/
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