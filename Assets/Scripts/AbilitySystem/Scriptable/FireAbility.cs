using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/FireAbility")]
    public class FireAbility : Ability
    {
        [SerializeField] private Scanner scannerPrefab;
        [SerializeField] private float scanRange = 10f;
        [SerializeField] private float scanDuration;
        [SerializeField] private GameObject scanVFX;
        [SerializeField] private GameObject damageVFX;

        public override void Activate()
        {
            if (!CommitAbility()) return;

            SetBoostDuration(scanDuration);

            Scanner fireScanner = Instantiate(scannerPrefab, AbilityComponent.transform);
            fireScanner.Init(this);
            fireScanner.SetScanRange(scanRange);
            fireScanner.SetScanDuration(scanDuration);
            Instantiate(scanVFX, fireScanner.Pivot);
            fireScanner.StartScan();
        }

        public void FireScannerOnScanDetectionUpdated(GameObject newDetection)
        {
            Debug.Log($"Detected: {newDetection.name}");
        }
    }
}