using Enums;
using SOContent;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyConfig _keyConfig;

    public KeyType KeyType { get; private set; }

    private void Start()
    {
        KeyType = _keyConfig.KeyType;
    }
}