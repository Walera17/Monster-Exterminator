namespace MonsterExterminator.AI.BehaviorTree
{
    public class BlackboardDecorator : Decorator
    {
        private readonly Blackboard blackboard;
        private readonly string key;
        private readonly RunCondition runCondition;                         // Условие выполнения
        private readonly NotifyRule notifyRule;                             // Правило Уведомления
        private readonly NotifyAbort notifyAbort;                           // Уведомить о прерывании
        private readonly BehaviorTree behaviorTree;
        private object value;

        public BlackboardDecorator(BehaviorTree behaviorTree, Node child, string key, RunCondition runCondition,
            NotifyRule notifyRule, NotifyAbort notifyAbort) : base(child)
        {
            this.behaviorTree = behaviorTree;
            blackboard = behaviorTree.Blackboard;
            this.key = key;
            this.runCondition = runCondition;
            this.notifyRule = notifyRule;
            this.notifyAbort = notifyAbort;
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
                if (value != valueParam)
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

        private void AbortSelf() => Abort();

        private void AbortLower() => behaviorTree.AbortLowerThan(Priority);

        private void AbortBoth()
        {
            Abort();
            AbortLower();
        }

        protected override NodeResult Execute()
        {
            blackboard.OnBlackboardValueChange -= Blackboard_OnBlackboardValueChange;

            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;

            if (CheckRunCondition())
                return NodeResult.Inprogress;

            return NodeResult.Failure;
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

        protected override NodeResult Update() => child.UpdateNode();

        protected override void End()
        {
            child.Abort();
            base.End();
        }

        public override string ToString() => GetType().Name;
    }
}