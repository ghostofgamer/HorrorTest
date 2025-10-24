using StarterAssets;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FirstPersonController _firstPersonController;
    
        public void SwitchController(bool value)
        {
            _firstPersonController.enabled = value;
        }
    }
}
