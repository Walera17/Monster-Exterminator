using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public class SightSense : Sense
    {
        [SerializeField] private float sightDistance = 7f;
        [SerializeField] private float sightHalfAngle = 30f;
        [SerializeField] private float eyeHeight = 1f;

        protected override bool InStimuliSensible(PerceptionStimuli stimuli)
        {
            Vector3 stimuliDirection = stimuli.transform.position - transform.position;
            if (stimuliDirection.sqrMagnitude > sightDistance * sightDistance)
                return false;

            if (Vector3.Angle(stimuliDirection.normalized, transform.forward) > sightHalfAngle)
                return false;

            return !Physics.Raycast(transform.position + Vector3.up * eyeHeight, stimuliDirection, out RaycastHit hitInfo, sightDistance) || hitInfo.collider.gameObject == stimuli.gameObject;
        }

        protected override void DrawDebug()
        {
            Gizmos.color = Color.cyan;
            Vector3 drawCenter = transform.position + Vector3.up * eyeHeight;
            Gizmos.DrawWireSphere(drawCenter, sightDistance);

            Vector3 leftLimitDir = Quaternion.AngleAxis(sightHalfAngle, Vector3.up) * transform.forward;
            Vector3 rightLimitDir = Quaternion.AngleAxis(-sightHalfAngle, Vector3.up) * transform.forward;

            Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDir * sightDistance);
            Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDir * sightDistance);
        }
    }
}