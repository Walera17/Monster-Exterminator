using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public static class GamePlayStatics
{
    private class AudioContext : MonoBehaviour
    {
    }

    private static readonly ObjectPool<AudioSource> audioPool; 

    static readonly AudioContext parentAudioPool;

    static GamePlayStatics()
    {
        parentAudioPool = new GameObject("PoolAudio").AddComponent<AudioContext>();
        audioPool = new(CreateAudioSource, null, null, DestroyAudioSource, false, 5, 7);
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