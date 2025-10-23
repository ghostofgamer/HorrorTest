using System.Collections;
using UnityEngine;

namespace DoorContent
{
    public class ParkingDoor : Door
    {
        [SerializeField] private float _targetZ = 180f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Transform doorTransform;
        [SerializeField] private bool useLocal = true;
        [SerializeField] private Animator _animator;

        private Coroutine _coroutine;

        public override void Open()
        {
            Debug.Log("Open");
            _animator.enabled = true;
            /*if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(StartOpen(doorTransform, _targetZ, _duration));*/
        }

        /*private IEnumerator StartOpen(Transform target, float targetAngle, float time)
        {
            /*float startZ = useLocal ? target.localEulerAngles.z : target.eulerAngles.z;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                float currentZ = Mathf.LerpAngle(startZ, targetAngle, Mathf.SmoothStep(0f, 1f, t));

                Vector3 euler = useLocal ? target.localEulerAngles : target.eulerAngles;
                euler.z = currentZ;

                if (useLocal)
                    target.localEulerAngles = euler;
                else
                    target.eulerAngles = euler;

                yield return null;
            }

            Vector3 finalEuler = useLocal ? target.localEulerAngles : target.eulerAngles;
            finalEuler.z = targetAngle;

            if (useLocal)
                target.localEulerAngles = finalEuler;
            else
                target.eulerAngles = finalEuler;#1#

        }*/
    }
}