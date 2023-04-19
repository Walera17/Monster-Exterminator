using UnityEngine;

namespace AI.BehaviorTree
{
    public class CooldownDecorator : Decorator
    {
        private readonly float cooldownTime;
        private readonly bool failOnCooldown;
        float lastExecutionTime = -1f;

        public CooldownDecorator(Node child, float cooldownTime, bool failOnCooldown = false) : base(child)
        {
            this.cooldownTime = cooldownTime;
            this.failOnCooldown = failOnCooldown;
        }

        protected override NodeResult Execute()
        {
            // first execution - первое исполнение
            if (cooldownTime == 0)
                return NodeResult.Inprogress;

            if (lastExecutionTime < 0)
            {
                lastExecutionTime = Time.timeSinceLevelLoad;
                return NodeResult.Inprogress;
            }

            // cooldown not finished - перезарядка не завершена
            if (Time.timeSinceLevelLoad - lastExecutionTime < cooldownTime)
            {
                if (failOnCooldown)
                    return NodeResult.Failure;

                return NodeResult.Success;
            }

            // cooldown is finished since last time - перезарядка завершена с прошлого раза
            lastExecutionTime = Time.timeSinceLevelLoad;
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            return child.UpdateNode();
        }
    }
}