namespace MonsterExterminator.AI.BehaviorTree
{
    public class TaskRemoveBlackboardData : Node
    {
        private readonly Blackboard blackboard;
        private readonly string keyToRemove;

        public TaskRemoveBlackboardData(Blackboard blackboard, string keyToRemove)
        {
            this.blackboard = blackboard;
            this.keyToRemove = keyToRemove;
        }

        protected override NodeResult Execute()
        {
            if (blackboard != null)
            {
                blackboard.RemoveBlackboardData(keyToRemove);
                return NodeResult.Success;
            }

            return NodeResult.Failure;
        }
    }
}