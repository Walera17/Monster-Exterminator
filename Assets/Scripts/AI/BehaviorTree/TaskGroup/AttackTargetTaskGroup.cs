namespace AI.BehaviorTree
{
    public class AttackTargetTaskGroup : TaskGroup
    {
        private readonly float moveAcceptableDistance;
        private readonly float rotateAcceptableDegrees;
        private readonly float attackCooldownDuration;

        public AttackTargetTaskGroup(BehaviorTree behaviorTree, float moveAcceptableDistance = 2f, float attackCooldownDuration = 0, float rotateAcceptableDegrees = 10f) : base(behaviorTree)
        {
            this.moveAcceptableDistance = moveAcceptableDistance;
            this.rotateAcceptableDegrees = rotateAcceptableDegrees;
            this.attackCooldownDuration = attackCooldownDuration;
        }

        protected override void ConstructTree(out Node root)
        {
            Sequencer attackTargetSequencer = new Sequencer();

            TaskMoveToTarget taskMoveToTarget = new TaskMoveToTarget(tree, "Target", moveAcceptableDistance);
            TaskRotateTowardsTarget taskRotateTowardsTarget = new TaskRotateTowardsTarget(tree, "Target", rotateAcceptableDegrees);
            TaskAttackTarget taskAttackTarget = new TaskAttackTarget(tree, "Target");
            CooldownDecorator attackCooldownDecorator = new CooldownDecorator(taskAttackTarget, attackCooldownDuration);

            attackTargetSequencer.AddChild(taskMoveToTarget);
            attackTargetSequencer.AddChild(taskRotateTowardsTarget);
            attackTargetSequencer.AddChild(attackCooldownDecorator);

            BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(tree,
                attackTargetSequencer, "Target",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            root = attackTargetDecorator;
        }
    }
}