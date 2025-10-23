using AttentionContent;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AttentionHintViewer _attentionHintViewer;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        AttentionHintActivator.Init(_attentionHintViewer);
    }
}