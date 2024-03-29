﻿using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Ability/SpeedBoost")]
    public class SpeedBoostAbility : Ability
    {
        [SerializeField] private float boostAmt = 20f;
        [SerializeField] private float durationBoost = 2f;

        public override void Activate()
        {
            AbilityComponent.AbilityOwner.AddMoveSpeed(boostAmt);
            AbilityComponent.StartCoroutine(ResetSpeed());
        }

        public override void Init(AbilityComponent component)
        {
            base.Init(component);
            SetBoostDuration(durationBoost);
        }

        private IEnumerator ResetSpeed()
        {
            yield return boostDurationWaitForSeconds;
            AbilityComponent.AbilityOwner.AddMoveSpeed(-boostAmt);

            StartAbilityCooldown();
        }
    }
}