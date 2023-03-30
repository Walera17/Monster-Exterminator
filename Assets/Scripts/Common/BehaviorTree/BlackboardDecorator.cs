using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class BlackboardDecorator : Decorator
    {
        private readonly Blackboard blackboard;
        private readonly string key;
        private readonly RunCondition runCondition;
        private readonly NotifyRule notifyRule;
        private readonly NotifyAbort notifyAbort;
        private Transform value;

        public BlackboardDecorator(BehaviorTree behaviorTree, Node child, string key, RunCondition runCondition,
            NotifyRule notifyRule, NotifyAbort notifyAbort) : base(child)
        {
            blackboard = behaviorTree.Blackboard;
            this.key = key;
            this.runCondition = runCondition;
            this.notifyRule = notifyRule;
            this.notifyAbort = notifyAbort;
            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;
        }

        private void Blackboard_OnBlackboardValueChange(string keyParam, object valueParam)
        {
            if (key != keyParam) return;

            if (notifyRule == NotifyRule.RunConditionChange)
            {
                bool prevExists = value != null;
                bool currentExists = valueParam != null;
                if (prevExists != currentExists)
                    Notify();
            }
            else if (notifyRule == NotifyRule.KeyValueChange)
            {
                if (value != null) 
                    Notify();
            }
        }

        private void Notify()
        {
            switch (notifyAbort)
            {
                case NotifyAbort.none:
                    break;
                case NotifyAbort.self:
                    AbortSelf();
                    break;
                case NotifyAbort.lower:
                    AbortLower();
                    break;
                case NotifyAbort.both:
                    AbortBoth();
                    break;
            }
        }

        private void AbortBoth()
        {
            Abort();
            AbortLower();
        }

        private void AbortLower()
        {
        }

        private void AbortSelf()
        {
            Abort();
        }

        protected override NodeResult Execute()
        {
            if (CheckRunCondition())
                return NodeResult.Inprogress;

            return NodeResult.Failure;
        }

        protected override NodeResult Update()
        {
            return Child.UpdateNode();
        }

        private bool CheckRunCondition()
        {
            bool exists = blackboard.GetBlackboardData(key, out value);
            return runCondition switch
            {
                RunCondition.KeyExists => exists,
                RunCondition.KeyNotExists => !exists,
                _ => false
            };
        }

        protected override void End()
        {
            Child.Abort();
            base.End();
        }

        ~BlackboardDecorator()
        {
            blackboard.OnBlackboardValueChange -= Blackboard_OnBlackboardValueChange;
        }
    }
}