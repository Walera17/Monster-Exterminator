using UnityEngine;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] private bool isRandom = true;
        [SerializeField] private Transform[] patrolPoints;
        int currentPatrolPointIndex = -1;

        private bool isValidatePatrolPoints;

        private void Start()
        {
            isValidatePatrolPoints = CheckPatrolPoints();
        }

        public bool GetRandomPatrolPoint(out Vector3 point)
        {
            if (!isValidatePatrolPoints)
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

        bool CheckPatrolPoints()
        {
            if (patrolPoints.Length == 0) return false;

            foreach (Transform point in patrolPoints)
            {
                if (point == null)
                    return false;
            }

            return true;
        }
    }
}