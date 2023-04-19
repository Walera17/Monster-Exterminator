using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Audio/UIAudioPlayer")]
    public class UIAudioPlayer : ScriptableObject
    {
        [SerializeField] private AudioClip clickAudioClip;
        [SerializeField] private AudioClip commitAudioClip;
        [SerializeField] private AudioClip selectedAudioClip;
        [SerializeField] private AudioClip winAudioClip;
        private AudioSource audioSource;

        public void PlayClick()
        {
            PlayAudio(clickAudioClip);
        }

        public void PlayCommit()
        {
            PlayAudio(commitAudioClip);
        }

        public void PlaySelected()
        {
            PlayAudio(selectedAudioClip);
        }

        void PlayAudio(AudioClip clip)
        {
            if (audioSource == null && Camera.main != null)
                audioSource = Camera.main.transform.GetComponent<AudioSource>();

            audioSource.PlayOneShot(clip);
        }

        public void PlayWinAudio()
        {
            PlayAudio(winAudioClip);
        }
    }
}