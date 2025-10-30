using System;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerGameInput : MonoBehaviour
    {
        public event Action ActionEvent;
        public event Action ThrowEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ActionEvent?.Invoke();

            if (Input.GetKeyDown(KeyCode.F))
                ThrowEvent?.Invoke();
        }
    }
}