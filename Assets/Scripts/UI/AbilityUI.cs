using AbilitySystem;
using System.Collections;
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

        public void Activate()
        {
            if (ability != null && !ability.AbilityOnCooldown)
            {
                ability.Activate();
                StartCoroutine(ShowDurationCoroutine(ability.BoostDuration, Color.green));
            }
        }

        private void OnDestroy()
        {
            ability.OnCooldownStarted -= Ability_OnCooldownStarted;
        }

        private void Ability_OnCooldownStarted()
        {
            StartCoroutine(ShowDurationCoroutine(ability.CooldownDuration, Color.yellow));
        }

        private IEnumerator ShowDurationCoroutine(float durationParam, Color color)
        {
            cooldownWheel.enabled = true;
            cooldownWheel.color = color;
            abilityIcon.color = color;
            float counter = durationParam;
            float duration = counter;
            while (counter > 0 || ability.AbilityOnCooldown)
            {
                counter -= Time.deltaTime;
                cooldownWheel.fillAmount = counter / duration;
                yield return null;
            }

            cooldownWheel.enabled = false;
            abilityIcon.color = Color.white;
        }
    }
}