namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskMoveToTarget : TaskMoveToPoint
    {
        public TaskMoveToTarget(BehaviorTree behaviorTree, string targetKey, float acceptableDistance) : base(behaviorTree, targetKey, acceptableDistance)
        {
        }

        protected override void End()
        {
            StopMovingAgent();
            blackboard.OnBlackboardValueChange -= Blackboard_OnBlackboardValueChange;
        }
    }
}