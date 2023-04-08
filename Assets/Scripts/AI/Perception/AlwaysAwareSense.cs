using UnityEngine;

namespace AI.Perception
{
    public class AlwaysAwareSense : Sense
    {
        [SerializeField] private float awareDistance = 2f;
        
        protected override bool InStimuliSensible(PerceptionStimuli stimuli)
        {
            return (transform.position - stimuli.transform.position).sqrMagnitude < awareDistance * awareDistance;
        }

        protected override void DrawDebug()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, awareDistance);
        }
    }
}