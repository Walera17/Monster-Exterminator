using Shop;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gamePlayControl;
        [SerializeField] private CanvasGroup gamePlayMenu;
        [SerializeField] PlayerStatisticUI statisticUI;
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private JoyStick moveStick;
        [SerializeField] private JoyStick aimStick;
        public JoyStick MoveStick => moveStick;
        public JoyStick AimStick => aimStick;

        public void SetGamePlayControlEnabled(bool enabledParam)
        {
            SetCanvasGroupEnabled(gamePlayControl, enabledParam);
        }
        public void SetGamePlayMenuEnabled(bool enabledParam)
        {
            SetCanvasGroupEnabled(gamePlayControl, enabledParam);
        }

        public void SetCanvasGroupEnabled(CanvasGroup canvasGroup, bool enabledParam)
        {
            canvasGroup.interactable = enabledParam;
            canvasGroup.blocksRaycasts = enabledParam;
        }

        public void SetHealthValue(float health, float maxHealth, float delta)
        {
            statisticUI.SetHealthValue(health, maxHealth, delta);
        }

        public void SetAbilityValue(float value, float maxValue, float delta)
        {
            statisticUI.SetAbilityValue(value, maxValue, delta);
        }

        public void InitShopUI(ShopSystem shopSystem, CreditComponent component)
        {
            shopUI.Init(shopSystem,component);
        }
    }
}