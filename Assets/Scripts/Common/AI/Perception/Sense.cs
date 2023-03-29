using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public abstract class Sense : MonoBehaviour
    {
        [SerializeField] private float forgettingTime = 2f;

        public delegate void OnPerceptionUpdateDelegate(PerceptionStimuli stimuli, bool successfullySensed);
        public event OnPerceptionUpdateDelegate OnPerceptionUpdate;

        static readonly List<PerceptionStimuli> registerStimuliList = new();        // зарегистрированные стимулы
        readonly List<PerceptionStimuli> perceivableStimuliList = new();            // воспринимаемые стимулы
        private WaitForSeconds forgettingWaitForSeconds;
        private Coroutine forgettingCoroutine;

        void Awake()
        {
            forgettingWaitForSeconds = new WaitForSeconds(forgettingTime);
        }

        public static void RegisterStimuli(PerceptionStimuli stimuli)
        {
            if (!registerStimuliList.Contains(stimuli))
                registerStimuliList.Add(stimuli);
        }

        public static void UnRegisterStimuli(PerceptionStimuli stimuli)
        {
            if (registerStimuliList.Contains(stimuli))
                registerStimuliList.Remove(stimuli);
        }

        protected abstract bool InStimuliSensible(PerceptionStimuli stimuli);

        private void Update()
        {
            foreach (PerceptionStimuli stimuli in registerStimuliList)
            {
                if (InStimuliSensible(stimuli))
                {
                    if (!perceivableStimuliList.Contains(stimuli))
                    {
                        perceivableStimuliList.Add(stimuli);
                        if (forgettingCoroutine != null)
                            StopCoroutine(forgettingCoroutine);
                        else
                            OnPerceptionUpdate?.Invoke(stimuli, true);
                    }
                }
                else if (perceivableStimuliList.Contains(stimuli))
                {
                    perceivableStimuliList.Remove(stimuli);
                    forgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
                }
            }
        }

        protected IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
        {
            yield return forgettingWaitForSeconds;
            forgettingCoroutine = null;
            OnPerceptionUpdate?.Invoke(stimuli, false);
        }

        private void OnDrawGizmosSelected()
        {
            DrawDebug();
        }

        protected virtual void DrawDebug()
        {
        }
    }
}