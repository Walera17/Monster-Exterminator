using UnityEngine;

namespace MonsterExterminator.AI.BehaviorTree
{
    public interface IBehaviorTreeInterface
    {
        public void RotateToward(GameObject target, bool verticalAim = false);
    }
}