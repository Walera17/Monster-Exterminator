using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskWaitNode : Node
    {
        private readonly float waitTime;
        private float timeElapsed;

        public TaskWaitNode(float waitTime)
        {
            this.waitTime = waitTime;
        }

        protected override NodeResult Execute()
        {
            if (waitTime <= 0)
                return NodeResult.Success;

            Debug.Log($"wait started with duration {waitTime}");
            timeElapsed = 0;
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= waitTime)
            {
                Debug.Log($"Wait finished");
                return NodeResult.Success;
            }

            Debug.Log($"Waiting for finished {timeElapsed}");
            return NodeResult.Inprogress;
        }

        protected override void End()
        {
            base.End();
        }
    }
}