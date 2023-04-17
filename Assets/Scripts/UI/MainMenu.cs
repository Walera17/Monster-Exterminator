using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] LevelManager levelManager;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            levelManager.LoadFirstLevel();
        }
    }
}