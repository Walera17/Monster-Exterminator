using System.Collections.Generic;
using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public class PerceptionComponent : MonoBehaviour
    {
        [SerializeField] private Sense[] senses;

        public delegate void OnPerceptionTargetChangedDelegate(Transform target, bool sensed);
        public event OnPerceptionTargetChangedDelegate OnPerceptionTargetChanged;

        private readonly LinkedList<PerceptionStimuli> currentlyPerceptionStimulus = new();
        private PerceptionStimuli targetStimuli;

        private void Start()
        {
            foreach (Sense sense in senses)
            {
                sense.OnPerceptionUpdate += Sense_OnPerceptionUpdate;
            }
        }

        private void Sense_OnPerceptionUpdate(PerceptionStimuli stimuli, bool successfullySensed)
        {
            LinkedListNode<PerceptionStimuli> nodeFound = currentlyPerceptionStimulus.Find(stimuli);

            if (successfullySensed)
            {
                if (nodeFound != null)
                    currentlyPerceptionStimulus.AddAfter(nodeFound, stimuli);
                else
                    currentlyPerceptionStimulus.AddLast(stimuli);
            }
            else if (nodeFound != null)
                currentlyPerceptionStimulus.Remove(nodeFound);

            if (currentlyPerceptionStimulus.Count != 0)
            {
                PerceptionStimuli highestStimuli = currentlyPerceptionStimulus.First.Value;
                if (targetStimuli == null || targetStimuli != highestStimuli)
                {
                    targetStimuli = highestStimuli;
                    OnPerceptionTargetChanged?.Invoke(targetStimuli.transform, true);
                }
            }
            else if (targetStimuli != null)
            {
                OnPerceptionTargetChanged?.Invoke(targetStimuli.transform, false);
                targetStimuli = null;
            }
        }
    }
}