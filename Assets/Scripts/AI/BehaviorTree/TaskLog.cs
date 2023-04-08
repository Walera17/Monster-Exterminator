using UnityEngine;

namespace AI.BehaviorTree
{
    public class TaskLog : Node
    {
        private readonly string message;

        public TaskLog(string message)
        {
            this.message = message;
        }

        protected override NodeResult Execute()
        {
            Debug.Log(message);
            return NodeResult.Success;
        }
    }
}