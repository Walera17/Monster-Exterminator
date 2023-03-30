namespace MonsterExterminator.Common.BehaviorTree
{
    public class BlackboardDecorator : Decorator
    {
        private readonly Blackboard blackboard;
        private string key;
        private RunCondition runCondition;
        private NotifyRule notifyRule;
        private NotifyAbort notifyAbort;

        public BlackboardDecorator(BehaviorTree behaviorTree, Node child, string key, RunCondition runCondition, NotifyRule notifyRule, NotifyAbort notifyAbort) : base(child)
        {
            blackboard = behaviorTree.Blackboard;
            this.key = key;
            this.runCondition = runCondition;
            this.notifyRule = notifyRule;
            this.notifyAbort = notifyAbort;
            blackboard.OnBlackboardValueChange += Blackboard_OnBlackboardValueChange;
        }

        private void Blackboard_OnBlackboardValueChange(string keyParam, object value)  
        {
            
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
            throw new System.NotImplementedException();
        }
    }
}