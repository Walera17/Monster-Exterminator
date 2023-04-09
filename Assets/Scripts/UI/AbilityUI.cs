using System;
using AbilitySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] private Image abilityIcon;
        [SerializeField] private Image cooldownWheel;

        private Ability ability;

        public void Init(Ability abilityParam)
        {
            ability = abilityParam; 
            abilityIcon.sprite = abilityParam.Icon;
            cooldownWheel.enabled = false;
            ability.OnCooldownStarted += Ability_OnCooldownStarted;
        }

        private void OnDestroy()
        {
            ability.OnCooldownStarted -= Ability_OnCooldownStarted;
        }

        private void Ability_OnCooldownStarted()
        {
            throw new System.NotImplementedException();
        }
    }
}