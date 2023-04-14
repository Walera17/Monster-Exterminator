using Characters.Health;
using Shop;
using System.Collections.Generic;
using Rewards;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityComponent : MonoBehaviour, IPurchaseListener
    {
        [SerializeField] Ability[] initialAbilities;
        [SerializeField] private float stamina = 200f;
        [SerializeField] private float maxStamina = 200f;

        readonly List<Ability> abilities = new();
        IAbilityInterface abilityInterface;

        public delegate void OnNewAbilityAddedDelegate(Ability ability);

        public event OnNewAbilityAddedDelegate OnNewAbilityAdded;
        public event HealthComponent.OnChangeParameterDelegate OnAbilityChange;

        public IAbilityInterface AbilityOwner => abilityInterface;

        private void Start()
        {
            abilityInterface = GetComponent<IAbilityInterface>();
            foreach (Ability ability in initialAbilities)
                GiveAbility(ability);
        }

        public void BroadcastStaminaValueImmediately()
        {
            OnAbilityChange?.Invoke(stamina, maxStamina, 0);
        }

        void GiveAbility(Ability ability)
        {
            Ability newAbility = Instantiate(ability);
            newAbility.Init(this);
            abilities.Add(ability);
            OnNewAbilityAdded?.Invoke(newAbility);
        }

        public void ActivateAbility(Ability ability)
        {
            if (abilities.Contains(ability))
            {
                ability.Activate();
            }
        }

        public bool TryConsumeStamina(float deltaValue)
        {
            if (stamina < deltaValue) return false;

            stamina -= deltaValue;
            OnAbilityChange?.Invoke(stamina, maxStamina, -deltaValue);
            return true;
        }

        public bool HandlePurchase(Object purchaseItem)
        {
            Ability ability = purchaseItem as Ability;
            if (ability == null) return false;

            GiveAbility(ability);
            return true;
        }

        public void Reward(Reward reward)
        {
            stamina = Mathf.Clamp(stamina + reward.stamina, 0, maxStamina);
            OnAbilityChange?.Invoke(stamina, maxStamina, reward.stamina);
        }
    }
}