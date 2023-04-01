using UnityEngine;
using UnityEngine.AI;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskMoveToLocation : Node
    {
        readonly string locationKey;
        readonly float acceptableDistanceSqr;
        readonly NavMeshAgent agent;
        Vector3 location;   
        protected readonly Blackboard blackboard;

        public TaskMoveToLocation(BehaviorTree behaviorTree, string locKey, float acceptableDistance = 0.25f)
        {
            agent = behaviorTree.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = acceptableDistance;
            locationKey = locKey;
            acceptableDistanceSqr = acceptableDistance * acceptableDistance;
            blackboard = behaviorTree.Blackboard;
        }

        protected void Blackboard_OnBlackboardValueChange(string key, object value)
        {
            if (key == locationKey)
                location = (Vector3)value;
        }

        protected override NodeResult Execute()
        {
            if (agent == null || !blackboard.GetBlackboardData(locationKey, out location))
                return NodeResult.Failure;

            if (IsTargetAcceptableDistance())
                return NodeResult.Success;

            agent.SetDestination(location);
            agent.isStopped = false;
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            if (!agent.hasPath)
                return NodeResult.Failure;

            if (ReachedDestination() || IsTargetAcceptableDistance())
            {
                agent.isStopped = true;
                return NodeResult.Success;
            }

            agent.SetDestination(location);

            return NodeResult.Inprogress;
        }

        bool IsTargetAcceptableDistance() => (location - agent.transform.position).sqrMagnitude <= acceptableDistanceSqr;

        bool ReachedDestination()
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0) return false;
            return true;
        }

        protected override void End()
        {
            agent.isStopped= true;
            base.End();
        }

        public override string ToString() => GetType().Name;
    }
}