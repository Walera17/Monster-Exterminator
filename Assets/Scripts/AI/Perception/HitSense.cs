using UnityEngine;

namespace AI.Perception
{
    public class HitSense : Sense
    {
        PerceptionStimuli perceptionStimuli;

        public void OnTakeDamage(GameObject instigator)
        {   
            perceptionStimuli = instigator.GetComponent<PerceptionStimuli>();
        }

        protected override bool InStimuliSensible(PerceptionStimuli stimuli)
        {
            if (perceptionStimuli != null && perceptionStimuli == stimuli)
            {
                perceptionStimuli = null;
                return true;
            }

            return false;
        }
    }
}