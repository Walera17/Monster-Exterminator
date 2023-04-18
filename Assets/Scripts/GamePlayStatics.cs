using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public static class GamePlayStatics
{
    private class AudioContext : MonoBehaviour
    {
    }

    static readonly ObjectPool<AudioSource> audioPool = new(CreateAudioSource, null, null, DestroyAudioSource, false, 5, 10);

    private static void DestroyAudioSource(AudioSource obj)
    {
        Object.Destroy(obj);
    }

    private static AudioSource CreateAudioSource()
    {
        GameObject newAudioGameObject = new GameObject("AudioSource", typeof(AudioSource), typeof(AudioContext));
        AudioSource audioSource = newAudioGameObject.GetComponent<AudioSource>();

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
        audioSource.GetComponent<AudioContext>().StartCoroutine(ReleaseAudioSource(audioSource, clip.length));
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
}