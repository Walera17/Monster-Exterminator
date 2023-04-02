using UnityEngine;

namespace MonsterExterminator.AI.BehaviorTree
{
    public interface IBehaviorTreeInterface
    {
        public void RotateToward(Transform target, bool verticalAim = false);
        public void AttackTarget(Transform target);
    }
}