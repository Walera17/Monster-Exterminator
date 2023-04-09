using System.Collections.Generic;
using AbilitySystem;
using UnityEngine;

namespace UI
{
    public class AbilityDock : MonoBehaviour
    {
        [SerializeField] private AbilityComponent abilityComponent;
        [SerializeField] private AbilityUI abilityUIPrefab;

        readonly List<AbilityUI> abilities = new();

        private void Start()
        {
            abilityComponent.OnNewAbilityAdded += AbilityComponent_OnNewAbilityAdded;
        }

        private void OnDestroy()
        {
            abilityComponent.OnNewAbilityAdded -= AbilityComponent_OnNewAbilityAdded;
        }

        private void AbilityComponent_OnNewAbilityAdded(Ability ability)
        {
            AbilityUI newAbilityUI = Instantiate(abilityUIPrefab, transform);
            newAbilityUI.Init(ability);
            abilities.Add(newAbilityUI);
        }
    }
}