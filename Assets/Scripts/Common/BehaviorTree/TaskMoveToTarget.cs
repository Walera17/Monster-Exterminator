using UnityEngine;
using UnityEngine.AI;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskMoveToTarget : Node
    {
        readonly string targetKey;
        readonly float acceptableDistanceSqr;
        readonly NavMeshAgent agent;
        Transform targetTransform;

        public TaskMoveToTarget(BehaviorTree behaviorTree, string targetKey, float acceptableDistance)
        {
            agent = behaviorTree.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = acceptableDistance;
            this.targetKey = targetKey;
            acceptableDistanceSqr = acceptableDistance * acceptableDistance;
            behaviorTree.Blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;
        }

        private void Blackboard_OnBlackboardValueChange(string key, object value)
        {
            if (key == targetKey)
                targetTransform = (Transform)value;
        }

        protected override NodeResult Execute()
        {
            if (agent == null || targetTransform == null) return NodeResult.Failure;

            if (IsTargetAcceptableDistance()) return NodeResult.Success;

            agent.SetDestination(targetTransform.position);

            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            if (targetTransform == null || !agent.hasPath)
                return NodeResult.Failure;

            if (ReachedDestination() || IsTargetAcceptableDistance())
            {
                StopMovingAgent();
                return NodeResult.Success;
            }

            agent.SetDestination(targetTransform.position);
            return NodeResult.Inprogress;
        }

        bool IsTargetAcceptableDistance() => (targetTransform.position - agent.transform.position).sqrMagnitude <=
                                             acceptableDistanceSqr;

        void StopMovingAgent() => agent.ResetPath();

        bool ReachedDestination()
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0) return false;
            return true;
        }
    }
}