using System.Collections.Generic;
using UnityEngine;

namespace Characters.AbilitySystem
{
    public class AbilityComponent : MonoBehaviour
    {
        [SerializeField] Ability[] initialAbilities;
        readonly List<Ability> abilities = new();

        public delegate void OnNewAbilityAddedDelegate(Ability ability);
        public event OnNewAbilityAddedDelegate OnNewAbilityAdded;

        private void Start()
        {
            foreach (Ability ability in initialAbilities)
            {
                GiveAbility(ability);
            }
        }

        void GiveAbility(Ability ability)
        {
            Ability newAbility = Instantiate(ability);
            newAbility.Init(this);
            abilities.Add(ability);
            OnNewAbilityAdded?.Invoke(newAbility);
        }
    }
}