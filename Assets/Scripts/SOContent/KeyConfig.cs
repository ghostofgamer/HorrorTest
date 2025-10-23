using Enums;
using UnityEngine;

namespace SOContent
{
    [CreateAssetMenu(menuName = "Keys/KeyType", fileName = "NewKeyType")]
    public class KeyConfig : ScriptableObject
    {
        [SerializeField] private KeyType _keyType;
        
        public KeyType KeyType => _keyType;
    }
}