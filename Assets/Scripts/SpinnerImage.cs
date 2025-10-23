using UnityEngine;
using UnityEngine.UI;

public class SpinnerImage : MonoBehaviour
{
    [SerializeField] private Image _spinnerImage; 
    [SerializeField] private float _rotationSpeed = 180f; 

    private void Update()
    {
        if (_spinnerImage != null)
            _spinnerImage.transform.Rotate(0f, 0f, -_rotationSpeed * Time.deltaTime);
    }
}