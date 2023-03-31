using UnityEngine;
using UnityEngine.AI;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskMoveToPoint : Node
    {
        readonly string targetKey;
        readonly float acceptableDistanceSqr;
        readonly NavMeshAgent agent;
        Transform targetTransform;
        protected readonly Blackboard blackboard;

        public TaskMoveToPoint(BehaviorTree behaviorTree, string targetKey, float acceptableDistance=0.25f)
        {
            agent = behaviorTree.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = acceptableDistance;
            this.targetKey = targetKey;
            acceptableDistanceSqr = acceptableDistance * acceptableDistance;
            blackboard = behaviorTree.Blackboard;
            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;
        }

        protected void Blackboard_OnBlackboardValueChange(string key, object value)
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

        bool IsTargetAcceptableDistance() => (targetTransform.position - agent.transform.position).sqrMagnitude <= acceptableDistanceSqr;

        protected void StopMovingAgent() => agent.ResetPath();

        bool ReachedDestination()
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0) return false;
            return true;
        }

        protected override void End()
        {
        }

        public override string ToString() => GetType().Name;
    }
}