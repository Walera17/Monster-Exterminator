using UnityEngine;

namespace MonsterExterminator.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gamePlayControl;
        [SerializeField] private CanvasGroup gamePlayMenu;
        [SerializeField] PlayerHealthBar healthBar;
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

        public void SetCanvasGroupEnabled(CanvasGroup canvasGroup,bool enabledParam)
        {
            canvasGroup.interactable = enabledParam;
            canvasGroup.blocksRaycasts = enabledParam;
        }

        public void SetHealthValue(float health, float maxHealth, float delta)
        {
            healthBar.SetHealthValue(health, maxHealth, delta);
        }
    }
}