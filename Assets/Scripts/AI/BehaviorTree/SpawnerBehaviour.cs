namespace AI.BehaviorTree
{
    public class SpawnerBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            TaskSpawn taskSpawn = new TaskSpawn(this);
            CooldownDecorator spawnCooldownDecorator = new CooldownDecorator(taskSpawn, 3.5f);
            BlackboardDecorator spawnBlackboardDecorator = new BlackboardDecorator(this, spawnCooldownDecorator,
                "Target", RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            rootNode = spawnBlackboardDecorator;
        }
    }
}