using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;

        public bool  GetRandomPatrolPoint(out Transform pointTransform)
        {
            if (patrolPoints.Length == 0)
            {
                pointTransform = transform;
                return false;
            }

            Transform newPoints;
            do
            {
                newPoints = patrolPoints[Random.Range(0, patrolPoints.Length)];
            } while ((transform.position - newPoints.position).sqrMagnitude < 1f);
            pointTransform = newPoints;

            return true;
        }
    }
}