using System.Collections;
using UnityEngine;

namespace MonsterExterminator.Damage
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField] private Transform shakeTransform;
        [SerializeField] private float shakeMagnitude = 0.1f;
        [SerializeField] private float shakeDuration = 0.1f;
        [SerializeField] private float shakeRecoverySpeed = 10f;

        private Coroutine shakeCoroutine;
        private bool isShaking;

        private Vector3 originalPosition;
        private WaitForSeconds waitShakeDuration;

        private void Start()
        {
            originalPosition = transform.localPosition;
            waitShakeDuration = new WaitForSeconds(shakeDuration);
        }

        public void StartShake()
        {
            if (shakeCoroutine == null)
            {
                isShaking = true;
                shakeCoroutine = StartCoroutine(ShakeStarted());
            }
        }

        private IEnumerator ShakeStarted()
        {
            yield return waitShakeDuration;
            isShaking = false;
            shakeCoroutine = null;
        }

        private void LateUpdate()
        {
            ProcessShake();
        }

        private void ProcessShake()
        {
            if (isShaking)
                shakeTransform.localPosition += new Vector3(Random.value, Random.value, Random.value) * shakeMagnitude * (Random.value > 0.5f ? -1 : 1);
            else
                shakeTransform.localPosition = Vector3.Lerp(shakeTransform.localPosition, originalPosition, shakeRecoverySpeed * Time.deltaTime);
        }
    }
}