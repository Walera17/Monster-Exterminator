using MonsterExterminator.Characters;
using MonsterExterminator.Characters.Enemies;

namespace MonsterExterminator.AI.BehaviorTree
{
    public class TaskSpawn : Node
    {
        private readonly SpawnerComponent spawnerComponent;

        public TaskSpawn(SpawnerComponent spawnerComponent)
        {
            this.spawnerComponent = spawnerComponent;
        }

        protected override NodeResult Execute()
        {
            if (spawnerComponent == null || !spawnerComponent.StartSpawn())
                return NodeResult.Failure;

            return NodeResult.Success;
        }
    }
}