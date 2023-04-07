namespace MonsterExterminator.AI.BehaviorTree
{
    public class LastSeenLocationTaskGroup : TaskGroup
    {
        private readonly float moveAcceptableDistance;

        public LastSeenLocationTaskGroup(BehaviorTree behaviorTree, float moveAcceptableDistance = 0.5f) : base(behaviorTree)
        {
            this.moveAcceptableDistance = moveAcceptableDistance;
        }

        protected override void ConstructTree(out Node root)
        {
            Sequencer checkLastSeenLocationSequencer = new Sequencer();

            TaskMoveToLocation moveToLastSeenLocation = new TaskMoveToLocation(tree, "LastSeenLocation", moveAcceptableDistance);
            TaskWait waitLastSeenLocation = new TaskWait(7f);
            TaskRemoveBlackboardData removeLastLocation = new TaskRemoveBlackboardData(tree.Blackboard, "LastSeenLocation");

            checkLastSeenLocationSequencer.AddChild(moveToLastSeenLocation);
            checkLastSeenLocationSequencer.AddChild(waitLastSeenLocation);
            checkLastSeenLocationSequencer.AddChild(removeLastLocation);

            BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(tree,
                checkLastSeenLocationSequencer, "LastSeenLocation",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.none);

            root = checkLastSeenLocationDecorator;
        }
    }
}