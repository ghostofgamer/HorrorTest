using System.Collections;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerRotationTarget : MonoBehaviour
    {
        public void RotateTowards(Transform target, float duration)
        {
            StartCoroutine(RotateRoutine(target, duration));
        }

        private IEnumerator RotateRoutine(Transform target, float duration)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(direction);
            float elapsed = 0f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                yield return null;
            }
            
            transform.rotation = endRotation;
        }
    }
}