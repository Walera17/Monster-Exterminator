using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterExterminator.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Image bar;
        [SerializeField] private TMP_Text health;
        [SerializeField] private TMP_Text amt;

        public void SetHealthValue(float amountHealth, float maxHealth, float delta)
        {
            bar.fillAmount = amountHealth / maxHealth;

            if (amountHealth > 0) health.text = amountHealth + "";
            else health.text = "";

            if (delta != 0 && bar.fillAmount > 0)
                amt.text = delta + "";
            else
                amt.text = "";
        }
    }
}