using MonsterExterminator.Characters;
using MonsterExterminator.Characters.Enemies;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class SpawnerBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            TaskSpawn taskSpawn = new TaskSpawn(GetComponent<SpawnerComponent>());
            CooldownDecorator spawnCooldownDecorator = new CooldownDecorator(taskSpawn, 7f);
            BlackboardDecorator spawnBlackboardDecorator = new BlackboardDecorator(this, spawnCooldownDecorator,
                "Target", RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            rootNode = spawnBlackboardDecorator;
        }
    }
}