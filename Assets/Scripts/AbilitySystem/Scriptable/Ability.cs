using System.Collections;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private float staminaCost = 10;
        [SerializeField] private float cooldownDuration = 2f;

        public delegate void OnCooldownStartedDelegate();
        public event OnCooldownStartedDelegate OnCooldownStarted;

        private float boostDuration;
        AbilityComponent abilityComponent;
        private bool abilityOnCooldown;
        WaitForSeconds cooldownWaitForSeconds;
        protected WaitForSeconds boostDurationWaitForSeconds;

        public AbilityComponent AbilityComponent => abilityComponent;

        public Sprite Icon => icon;

        public bool AbilityOnCooldown => abilityOnCooldown;

        public float CooldownDuration => cooldownDuration;

        public float BoostDuration => boostDuration;

        public void SetBoostDuration(float value)
        {
            boostDurationWaitForSeconds = new WaitForSeconds(value);
            boostDuration = value;
        }
        public virtual void Init(AbilityComponent component)
        {
            abilityComponent = component;
            cooldownWaitForSeconds = new WaitForSeconds(cooldownDuration);
        }

        public abstract void Activate();

        public bool CommitAbility()
        {
            if (abilityOnCooldown) return false;

            if (abilityComponent == null || !abilityComponent.TryConsumeStamina(staminaCost)) return false;

            return true;
        }

        protected void StartAbilityCooldown()
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