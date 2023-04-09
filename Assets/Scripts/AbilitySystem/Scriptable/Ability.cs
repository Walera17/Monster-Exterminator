using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private float staminaCost = 10;
        [SerializeField] private float cooldownDuration = 2f;
        [SerializeField] private float boostDuration = 2f;

        public delegate void OnCooldownStartedDelegate();

        public event OnCooldownStartedDelegate OnCooldownStarted;

        AbilityComponent abilityComponent;
        private bool abilityOnCooldown;
        WaitForSeconds cooldownWaitForSeconds;
        protected WaitForSeconds boostDurationWaitForSeconds;

        public AbilityComponent AbilityComponent => abilityComponent;

        public Sprite Icon => icon;

        public void Init(AbilityComponent component)
        {
            abilityComponent = component;
            cooldownWaitForSeconds = new WaitForSeconds(cooldownDuration);
            boostDurationWaitForSeconds = new WaitForSeconds(boostDuration);
        }

        public abstract void Activate();

        protected bool CommitAbility()
        {
            if (abilityOnCooldown) return false;

            if (abilityComponent == null || !abilityComponent.TryConsumeStamina(staminaCost)) return false;

            StartAbilityCooldown();

            return true;
        }

        void StartAbilityCooldown()
        {
            abilityComponent.StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine()
        {
            abilityOnCooldown = true;
            OnCooldownStarted?.Invoke();
            yield return cooldownWaitForSeconds;
            abilityOnCooldown = false;
        }
    }
}