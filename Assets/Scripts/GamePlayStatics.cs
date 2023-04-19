using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public static class GamePlayStatics
{
    private class AudioContext : MonoBehaviour
    {
    }

    static ObjectPool<AudioSource> audioPool;
    static AudioContext parentAudioPool;
    static int enemyCount;

    public delegate void OnLevelFinishedDelegate();

    public static event OnLevelFinishedDelegate OnLevelFinished;

    public static void Restart()
    {
        parentAudioPool = new GameObject("PoolAudio").AddComponent<AudioContext>();
        audioPool = new(CreateAudioSource, null, null, DestroyAudioSource, false, 5, 7);
    }

    public static int EnemyCount
    {
        get => enemyCount;
        set
        {
            enemyCount = value;
            if (enemyCount == 0)
                OnLevelFinished?.Invoke();
        }
    }

    private static void DestroyAudioSource(AudioSource obj)
    {
        Object.Destroy(obj.gameObject);
    }

    private static AudioSource CreateAudioSource()
    {
        AudioSource audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        audioSource.transform.SetParent(parentAudioPool.transform);

        audioSource.volume = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1f;

        return audioSource;
    }

    public static void PlayAudioAtLocation(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource audioSource = audioPool.Get();
        audioSource.volume = volume;
        audioSource.transform.position = position;
        audioSource.PlayOneShot(clip);
        parentAudioPool.StartCoroutine(ReleaseAudioSource(audioSource, clip.length));
    }

    private static IEnumerator ReleaseAudioSource(AudioSource audioSource, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        audioPool.Release(audioSource);
    }

    public static void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }

    public static void PlayAudioAtPlayer(AudioClip clip, float volume)
    {
        PlayAudioAtLocation(clip, Camera.main!.transform.position, volume);
    }
}