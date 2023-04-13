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

        public void Init(UIManager manager)
        {
            uiManager = manager;
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