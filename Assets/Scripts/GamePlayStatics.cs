using UnityEngine;

public static class GamePlayStatics
{
    public static void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 1 : 0;
    }
}