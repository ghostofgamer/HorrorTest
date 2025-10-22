using Enums;
using UnityEngine;

namespace ItemContent
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;

        public ItemType ItemType => _itemType;
    }
}