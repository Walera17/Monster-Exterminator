using UnityEngine;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] private float staminaCost =10;
        AbilityComponent abilityComponent;
        private bool abilityOnCooldown;
        private float abilityCooldownTime;

        public void Init(AbilityComponent component)
        {
            abilityComponent = component;
        }

        public abstract void Activate();

        protected bool CommitAbility(Ability ability)
        {
            if(abilityOnCooldown) return false;

            if(abilityComponent == null || !abilityComponent.TryConsumeStamina(staminaCost)) return false;

            return true;
        }
    }
}