using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private float fireRadius;
        [SerializeField] private float scanDuration;
        private FireAbility fireAbility;

        public Transform Pivot => pivot;

        public void Init(FireAbility ability)
        {
            fireAbility = ability;
        }

        public void SetScanRange(float scanRange)
        {
            fireRadius = scanRange;
        }

        public void SetScanDuration(float duration)
        {
            scanDuration = duration;
        }

        public void StartScan()
        {
            pivot.localScale = Vector3.zero;
            StartCoroutine(StartScanCoroutine());
        }

        private IEnumerator StartScanCoroutine()
        {
            float scanGrowthRate = fireRadius / scanDuration;
            float startTime = 0;
            while (startTime < scanDuration)
            {
                startTime += Time.deltaTime;
                pivot.localScale += Vector3.one * (scanGrowthRate * Time.deltaTime);
                yield return null;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            fireAbility.FireScannerOnScanDetectionUpdated(other.gameObject);
        }
    }
}