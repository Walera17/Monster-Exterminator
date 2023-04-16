using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private UIManager uiManager;
        [SerializeField] LevelManager levelManager;

        private void Start()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartLevel);
            menuButton.onClick.AddListener(BackToMainMenu);
        }

        private void ResumeGame()
        {
            uiManager.SwitchToGamePlayControl();
        }

        private void RestartLevel()
        {
            levelManager.RestartCurrentLevel();
        }

        private void BackToMainMenu()
        {
            levelManager.GoToMainMenu();
        }
    }
}