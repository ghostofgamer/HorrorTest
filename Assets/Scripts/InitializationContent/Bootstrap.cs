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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        AttentionHintActivator.Init(_attentionHintViewer);
    }
}