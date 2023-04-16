using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    [CreateAssetMenu(menuName = "LevelManager")]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private int mainMenuIndex = 0;
        [SerializeField] private int firstLevelIndex = 1;

        public void GoToMainMenu()
        {
            LoadSceneByIndex(mainMenuIndex);
        }

        public void LoadFirstLevel()
        {
            LoadSceneByIndex(firstLevelIndex);
        }

        public void RestartCurrentLevel()
        {
            LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
        }

        void LoadSceneByIndex(int index)
        {
            SceneManager.LoadScene(index);
            GamePlayStatics.SetGamePaused(false);
        }
    }
}