namespace AI.BehaviorTree
{
    public class TaskSpawn : Node
    {
        private readonly IBehaviorTreeInterface behaviorTreeInterface;

        public TaskSpawn(BehaviorTree behaviorTree)
        {
            behaviorTreeInterface = behaviorTree.GetBehaviorTreeInterface();
        }

        protected override NodeResult Execute()
        {
            if (!behaviorTreeInterface.StartSpawn())
                return NodeResult.Failure;

            return NodeResult.Success;
        }
    }
}