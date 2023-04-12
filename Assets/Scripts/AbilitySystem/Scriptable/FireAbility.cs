using System.Collections;
using Characters;
using Characters.Damage;
using Characters.Health;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/FireAbility")]
    public class FireAbility : Ability
    {
        [SerializeField] private Scanner scannerPrefab;
        [SerializeField] private float fireRadius = 20f;
        [SerializeField] private float fireDuration = 0.5f;
        [SerializeField] private float damageDuration = 3f;
        [SerializeField] private float fireDamage = 60f;
        [SerializeField] private GameObject scanVFX;
        [SerializeField] private GameObject damageVFX;

        public override void Activate()
        {
            SetBoostDuration(fireDuration);

            Scanner fireScanner = Instantiate(scannerPrefab, AbilityComponent.transform);
            fireScanner.Init(this);
            fireScanner.SetScanRange(fireRadius);
            fireScanner.SetScanDuration(fireDuration);
            Instantiate(scanVFX, fireScanner.Pivot);
            fireScanner.StartScan();

            AbilityComponent.StartCoroutine(StartCooldownFire());
        }

        private IEnumerator StartCooldownFire()
        {
            yield return boostDurationWaitForSeconds;

            StartAbilityCooldown();
        }

        public void FireScannerOnScanDetectionUpdated(GameObject newDetection)
        {
            if (newDetection.TryGetComponent(out ITeamInterface teamInterface) &&
                teamInterface.GetRelationTowards(AbilityComponent.gameObject) != TeamRelation.Enemy)
                return;

            if (newDetection.TryGetComponent(out HealthComponent healthComponent))
                AbilityComponent.StartCoroutine(ApplyDamageTo(healthComponent));
        }

        private IEnumerator ApplyDamageTo(HealthComponent healthComponent)
        {
            GameObject damageEffect = Instantiate(damageVFX, healthComponent.transform);
            float damageRate = fireDamage / damageDuration;
            float startTime = 0;

            while (startTime < damageDuration && healthComponent != null)
            {
                startTime += Time.deltaTime;
                healthComponent.ChangeHealth(-damageRate * Time.deltaTime, AbilityComponent.gameObject);
                yield return null;
            }

            if (damageEffect != null)
                Destroy(damageEffect);
        }
    }
}