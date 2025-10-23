using System;
using UnityEngine;

namespace PlayerContent
{
    public class Player : MonoBehaviour
    {
        public event Action Died;

        public void Die() => Died?.Invoke();
    }
}