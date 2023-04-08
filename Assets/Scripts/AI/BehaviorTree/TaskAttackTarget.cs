using UnityEngine;

namespace AI.BehaviorTree
{
    public class TaskAttackTarget : Node
    {
        readonly string targetKey;
        readonly Blackboard blackboard;
        readonly IBehaviorTreeInterface behaviorTreeInterface;
        Transform target;

        public TaskAttackTarget(BehaviorTree behaviorTree,string targetKey)
        {
            this.targetKey = targetKey;
            blackboard = behaviorTree.Blackboard;
            behaviorTree.GetNavMeshAgent();
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

            behaviorTreeInterface.AttackTarget(target);
            return NodeResult.Success;
        }
    }
}