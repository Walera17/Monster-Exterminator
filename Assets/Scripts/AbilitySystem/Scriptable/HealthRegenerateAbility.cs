using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/HealthRegenerate")]
    public class HealthRegenerateAbility : Ability
    {
        [SerializeField] private float healthRegenerateAmount = 50f;
        [SerializeField] private float speedRegenerate = 20f;
        private float deltaHealth;

        public override void Activate()
        {
            SetBoostDuration(deltaHealth / speedRegenerate);

            AbilityComponent.AbilityOwner.HealthRegenerate(healthRegenerateAmount, speedRegenerate);

            AbilityComponent.StartCoroutine(StartCooldownHealthRegenerate());
        }

        public override bool CommitAbility()
        {
            deltaHealth = AbilityComponent.AbilityOwner.GetDeltaHealth(healthRegenerateAmount);
            if (deltaHealth == 0) return false;

            return base.CommitAbility();
        }

        private IEnumerator StartCooldownHealthRegenerate()
        {
            yield return boostDurationWaitForSeconds;

            StartAbilityCooldown();
        }
    }
}