namespace MonsterExterminator.AI.BehaviorTree
{
    public class AttackTargetTaskGroup : TaskGroup
    {
        private readonly float moveAcceptableDistance;
        private readonly float rotateAcceptableDegrees;

        public AttackTargetTaskGroup(BehaviorTree behaviorTree, float moveAcceptableDistance = 2f, float rotateAcceptableDegrees = 10f) : base(behaviorTree)
        {
            this.moveAcceptableDistance = moveAcceptableDistance;
            this.rotateAcceptableDegrees = rotateAcceptableDegrees;
        }

        protected override void ConstructTree(out Node root)
        {
            Sequencer attackTargetSequencer = new Sequencer();

            TaskMoveToTarget taskMoveToTarget = new TaskMoveToTarget(tree, "Target", moveAcceptableDistance);
            TaskRotateTowardsTarget taskRotateTowardsTarget = new TaskRotateTowardsTarget(tree, "Target", rotateAcceptableDegrees);
            TaskAttackTarget taskAttackTarget = new TaskAttackTarget(tree, "Target");

            attackTargetSequencer.AddChild(taskMoveToTarget);
            attackTargetSequencer.AddChild(taskRotateTowardsTarget);
            attackTargetSequencer.AddChild(taskAttackTarget);

            BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(tree,
                attackTargetSequencer, "Target",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            root = attackTargetDecorator;
        }
    }
}