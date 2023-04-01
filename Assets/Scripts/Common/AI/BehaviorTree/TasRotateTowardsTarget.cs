using UnityEngine;
using UnityEngine.AI;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class TaskRotateTowardsTarget : Node
    {
        readonly string targetKey;
        private readonly float acceptableDegrees;
        readonly NavMeshAgent agent;
        readonly Blackboard blackboard;
        readonly IBehaviorTreeInterface behaviorTreeInterface;
        private Transform target;

        public TaskRotateTowardsTarget(BehaviorTree behaviorTree, string targetKey, float acceptableDegrees)
        {
            this.targetKey = targetKey;
            this.acceptableDegrees = acceptableDegrees;
            agent = behaviorTree.GetNavMeshAgent();
            blackboard = behaviorTree.Blackboard;
            behaviorTreeInterface = behaviorTree.GetBehaviorTreeInterface();
        }

        protected override NodeResult Execute()
        {
            if (blackboard == null)
                return NodeResult.Failure;

            if (behaviorTreeInterface == null)
                return NodeResult.Failure;

            if (!blackboard.GetBlackboardData(targetKey, out target))
                return NodeResult.Failure;

            agent.updateRotation = false;

            if (IsInAcceptableDegrees())
                return NodeResult.Success;

            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;

            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            if (blackboard == null)
                return NodeResult.Failure;

            if (IsInAcceptableDegrees())
                return NodeResult.Success;

            behaviorTreeInterface.RotateToward(target);
            return NodeResult.Inprogress;
        }

        private void Blackboard_OnBlackboardValueChange(string key, object value)
        {
            if (key == targetKey)
                target = (Transform)value;
        }

        bool IsInAcceptableDegrees()
        {
            if (target == null) return false;

            Vector3 targetDir = target.position - agent.transform.position;

            return Vector3.Angle(targetDir, agent.transform.forward) <= acceptableDegrees;
        }

        protected override void End()
        {
            blackboard.OnBlackboardValueChange -= Blackboard_OnBlackboardValueChange;
            agent.updateRotation = true;
            base.End();
        }
    }
}