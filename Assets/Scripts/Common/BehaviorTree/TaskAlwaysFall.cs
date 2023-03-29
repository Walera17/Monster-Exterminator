using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskAlwaysFall : Node
    {
        protected override NodeResult Execute()
        {
            Debug.Log("Failed");
            return NodeResult.Failure;
        }
    }
}