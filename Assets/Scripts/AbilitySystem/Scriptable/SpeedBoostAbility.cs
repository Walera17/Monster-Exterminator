using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/SpeedBoost")]
    public class SpeedBoostAbility : Ability
    {
        [SerializeField] private float boostAmt = 20f;
        [SerializeField] private float boostDuration = 2f;

        public override void Init(AbilityComponent component)
        {
            base.Init(component);
            boostDurationWaitForSeconds = new WaitForSeconds(boostDuration);
        }

        public override void Activate()
        {
            if (!CommitAbility()) return;

            AbilityComponent.AbilityOwner.AddMoveSpeed(boostAmt);
            AbilityComponent.StartCoroutine(ResetSpeed());
        }

        private IEnumerator ResetSpeed()
        {
            yield return boostDurationWaitForSeconds;
            AbilityComponent.AbilityOwner.AddMoveSpeed(-boostAmt);

            StartAbilityCooldown();
        }
    }
}