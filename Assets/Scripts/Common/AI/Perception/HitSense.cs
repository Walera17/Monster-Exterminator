using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public class HitSense : Sense
    {
        [SerializeField] HealthComponent healthComponent;
        PerceptionStimuli perceptionStimuli;

        void Start()
        {
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
        }

        private void HealthComponent_OnTakeDamage(float health, float maxHealth, GameObject instigator)
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