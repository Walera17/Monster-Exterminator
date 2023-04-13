using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerCreditUI : MonoBehaviour
    {
        [SerializeField] private Button shopButton;
        [SerializeField] private TMP_Text creditText;

        private UIManager uiManager;
        CreditComponent creditComponent;

        public void Init(UIManager manager, CreditComponent component)
        {
            uiManager = manager;
            creditComponent = component;

            creditComponent.OnCreditChanged += UpdateCredit;
            UpdateCredit(component.Credit);
        }

        private void OnDestroy()
        {
            creditComponent.OnCreditChanged -= UpdateCredit;
        }

        private void UpdateCredit(int newCredit)
        {
            creditText.text = newCredit.ToString();
        }

        private void Start()
        {
            shopButton.onClick.AddListener(PullOutShop);
        }

        private void PullOutShop()
        {
            uiManager.SwitchToShop();
        }
    }
}