using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskGetNextPatrolPoint : Node
    {
        private readonly Blackboard blackboard;
        private readonly string patrolPointKey;
        private readonly PatrolComponent patrolComponent;

        public TaskGetNextPatrolPoint(BehaviorTree behaviorTree, string patrolPointKey)
        {
            blackboard = behaviorTree.Blackboard;
            this.patrolPointKey = patrolPointKey;
            patrolComponent = behaviorTree.GetComponent<PatrolComponent>();
        }

        protected override NodeResult Execute()
        {
            if (patrolComponent.GetRandomPatrolPoint(out Transform point))
            {
                blackboard.SetOrAddData(patrolPointKey, point);
                return NodeResult.Success;
            }

            return NodeResult.Failure;
        }
    }
}