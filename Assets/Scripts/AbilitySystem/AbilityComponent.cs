using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityComponent : MonoBehaviour
    {
        [SerializeField] Ability[] initialAbilities;
        [SerializeField] private float stamina = 200f;
        [SerializeField] private float maxStamina = 200f;

        readonly List<Ability> abilities = new();
        IAbilityInterface abilityInterface;

        public delegate void OnNewAbilityAddedDelegate(Ability ability);

        public delegate void OnAbilityChangeDelegate(float value, float maxValue);

        public event OnNewAbilityAddedDelegate OnNewAbilityAdded;
        public event OnAbilityChangeDelegate OnAbilityChange;

        //public float Stamina => stamina;

        public IAbilityInterface AbilityOwner => abilityInterface;

        private void Start()
        {
            abilityInterface = GetComponent<IAbilityInterface>();
            foreach (Ability ability in initialAbilities)
                GiveAbility(ability);
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

        public bool TryConsumeStamina(float value)
        {
            if (stamina < value) return false;

            stamina -= value;
            OnAbilityChange?.Invoke(stamina, maxStamina);
            return true;
        }
    }
}