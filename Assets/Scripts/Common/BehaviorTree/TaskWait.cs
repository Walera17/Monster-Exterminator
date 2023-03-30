using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class TaskWait : Node
    {
        private readonly float waitTime;
        private float timeElapsed;

        public TaskWait(float waitTime)
        {
            this.waitTime = waitTime;
        }

        protected override NodeResult Execute()
        {
            if (waitTime <= 0)
                return NodeResult.Success;

            //Debug.Log($"wait started with duration {waitTime}");
            timeElapsed = 0;
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= waitTime)
            {
                //Debug.Log($"Wait finished");
                return NodeResult.Success;
            }

            //Debug.Log($"Waiting for finished {timeElapsed}");
            return NodeResult.Inprogress;
        }
    }
}