using UnityEngine;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] private bool isRandom = true;
        [SerializeField] private Transform[] patrolPoints;
        int currentPatrolPointIndex = -1;

        public bool GetRandomPatrolPoint(out Vector3 point)
        {
            if (patrolPoints.Length == 0)
            {
                point = transform.position;
                return false;
            }

            if (isRandom)
            {
                Transform newPoints;
                do
                {
                    newPoints = patrolPoints[Random.Range(0, patrolPoints.Length)];
                } while ((transform.position - newPoints.position).sqrMagnitude < 1f);

                point = newPoints.position;
            }
            else
            {
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
                point = patrolPoints[currentPatrolPointIndex].position;
            }

            return true;
        }
    }
}