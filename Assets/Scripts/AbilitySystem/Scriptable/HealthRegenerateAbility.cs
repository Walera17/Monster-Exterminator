using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/HealthRegenerate")]
    public class HealthRegenerateAbility : Ability
    {
        [SerializeField] private float healthRegenerateAmount = 50f;
        [SerializeField] private float speedRegenerate = 20f;

        public override void Activate()
        {
            float deltaHealth = AbilityComponent.AbilityOwner.GetDeltaHealth(healthRegenerateAmount);
            if (deltaHealth == 0) return;
            if (!CommitAbility()) return;

            SetBoostDuration(deltaHealth / speedRegenerate);

            AbilityComponent.AbilityOwner.HealthRegenerate(healthRegenerateAmount, speedRegenerate);

            AbilityComponent.StartCoroutine(StartCooldownHealthRegenerate());
        }

        private IEnumerator StartCooldownHealthRegenerate()
        {
            yield return boostDurationWaitForSeconds;

            StartAbilityCooldown();
        }
    }
}