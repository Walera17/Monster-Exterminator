using Shop;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gamePlayControl;
        [SerializeField] private CanvasGroup pauseMenu;
        [SerializeField] private CanvasGroup deathMenu;
        [SerializeField] private CanvasGroup winMenu;
        [SerializeField] private CanvasGroup shop;
        [SerializeField] PlayerStatisticUI statisticUI;
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private PlayerCreditUI playerCreditUI;
        [SerializeField] private JoyStick moveStick;
        [SerializeField] private JoyStick aimStick;
        [SerializeField] private UIAudioPlayer uiAudioPlayer;

        public JoyStick MoveStick => moveStick;
        public JoyStick AimStick => aimStick;

        readonly List<CanvasGroup> allChildren = new();

        private void Start()
        {
            List<CanvasGroup> allCanvasGroups = new List<CanvasGroup>();
            GetComponentsInChildren(true, allCanvasGroups);
            foreach (CanvasGroup child in allCanvasGroups)
            {
                if (child.transform.parent == transform)
                    allChildren.Add(child);
            }

            SetCurrentActiveGroup(gamePlayControl);

            GamePlayStatics.OnLevelFinished += GamePlayStatics_OnLevelFinished;
        }

        private void OnDestroy()
        {
            GamePlayStatics.OnLevelFinished -= GamePlayStatics_OnLevelFinished;
        }

        private void GamePlayStatics_OnLevelFinished()
        {
            SetCurrentActiveGroup(winMenu);
            GamePlayStatics.SetGamePaused(true);
            uiAudioPlayer.PlayWinAudio();
        }

        public void SetCurrentActiveGroup(CanvasGroup group)
        {
            foreach (CanvasGroup child in allChildren)
            {
                if (child == group)
                    SetVisibleGroup(child, true, true);
                else
                    SetVisibleGroup(child, false, false);
            }
        }

        private void SetVisibleGroup(CanvasGroup canvasGroup, bool interactable, bool visible)
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
            canvasGroup.alpha = visible ? 1 : 0;
        }

        public void SwitchToPauseMenu()
        {
            SetCurrentActiveGroup(pauseMenu);
            GamePlayStatics.SetGamePaused(true);
        }

        public void SetHealthValue(float health, float maxHealth, float delta)
        {
            statisticUI.SetHealthValue(health, maxHealth, delta);
        }

        public void SetAbilityValue(float value, float maxValue, float delta)
        {
            statisticUI.SetAbilityValue(value, maxValue, delta);
        }

        public void InitShop(ShopSystem shopSystem, CreditComponent component)
        {
            shopUI.Init(this, shopSystem, component);
            playerCreditUI.Init(this, component);
        }

        public void SwitchToShop()
        {
            SetCurrentActiveGroup(shop);
            GamePlayStatics.SetGamePaused(true);
        }

        public void SwitchToGamePlayControl()
        {
            SetCurrentActiveGroup(gamePlayControl);
            GamePlayStatics.SetGamePaused(false);
        }

        public void SwitchToDeathMenu()
        {
            SetCurrentActiveGroup(deathMenu);
            GamePlayStatics.SetGamePaused(true);
        }
    }
}