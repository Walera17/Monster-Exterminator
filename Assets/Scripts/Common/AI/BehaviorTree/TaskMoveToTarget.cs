using UnityEngine;
using UnityEngine.AI;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class TaskMoveToTarget : Node
    {
        readonly string targetKey;
        readonly float acceptableDistanceSqr;
        readonly NavMeshAgent agent;
        Transform targetTransform;
        protected readonly Blackboard blackboard;
        private readonly float speedAgent;

        public TaskMoveToTarget(BehaviorTree behaviorTree, string targetKey, float acceptableDistance = 0.25f)
        {
            agent = behaviorTree.GetNavMeshAgent();
            speedAgent = agent.speed;
            agent.stoppingDistance = acceptableDistance;
            this.targetKey = targetKey;
            acceptableDistanceSqr = acceptableDistance * acceptableDistance;
            blackboard = behaviorTree.Blackboard;
        }

        protected void Blackboard_OnBlackboardValueChange(string key, object value)
        {
            if (key == targetKey)
                targetTransform = (Transform)value;
        }

        protected override NodeResult Execute()
        {
            if (agent == null || !blackboard.GetBlackboardData(targetKey, out targetTransform))
                return NodeResult.Failure;

            if (IsTargetAcceptableDistance())
                return NodeResult.Success;

            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;

            agent.SetDestination(targetTransform.position);
            SetChaseSpeed(true);
            agent.isStopped = false;
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            if (targetTransform == null)
            {
                agent.isStopped = true;
                return NodeResult.Failure;
            }

            agent.SetDestination(targetTransform.position);

            if (ReachedDestination() || IsTargetAcceptableDistance())
            {
                agent.isStopped = true;
                return NodeResult.Success;
            }

            SetChaseSpeed(true);
            return NodeResult.Inprogress;
        }

        bool IsTargetAcceptableDistance() => (targetTransform.position - agent.transform.position).sqrMagnitude <=
                                             acceptableDistanceSqr;

        bool ReachedDestination()
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0) return false;
            return true;
        }

        protected override void End()
        {
            agent.isStopped = true;
            SetChaseSpeed(false);
            blackboard.OnBlackboardValueChange -= Blackboard_OnBlackboardValueChange;
            base.End();
        }

        private void SetChaseSpeed(bool chase)
        {
            agent.speed = chase ? speedAgent * 1.75f : speedAgent;
        }
    }
}