using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatisticUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text health;
        [SerializeField] private TMP_Text deltaHealth;
        [SerializeField] private Image staminaBar;
        [SerializeField] private TMP_Text stamina;
        [SerializeField] private TMP_Text deltaStamina;
        [SerializeField] private float showDelta = 0.5f;
        private WaitForSeconds showDeltaWaitForSeconds;

        private void Start()
        {
            showDeltaWaitForSeconds = new WaitForSeconds(showDelta);
        }

        public void SetHealthValue(float amountHealth, float maxHealth, float delta)
        {
            healthBar.fillAmount = amountHealth / maxHealth;

            if (amountHealth > 0) health.text = (int)amountHealth + "";
            else health.text = "";

            if (delta != 0 && healthBar.fillAmount > 0)
                deltaHealth.text = delta + "";
            else
                deltaHealth.text = "";

            StartCoroutine(HideDeltaCoroutine(deltaHealth));
        }

        public void SetAbilityValue(float staminaValue, float maxStamina, float delta)
        {
            staminaBar.fillAmount = staminaValue / maxStamina;
            deltaStamina.color = delta > 0 ? Color.green : Color.red;
            if (staminaValue > 0) stamina.text = staminaValue + "";
            else stamina.text = "";

            if (delta != 0 && staminaBar.fillAmount > 0)
                deltaStamina.text = delta + "";
            else
                deltaStamina.text = "";

            StartCoroutine(HideDeltaCoroutine(deltaStamina));
        }

        private IEnumerator HideDeltaCoroutine(TMP_Text tmpText)
        {
            yield return showDeltaWaitForSeconds;
            tmpText.text = "";
        }
    }
}