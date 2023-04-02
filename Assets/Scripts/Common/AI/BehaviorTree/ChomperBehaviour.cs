namespace MonsterExterminator.AI.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            Selector rootSelector = new Selector();

            #region attackTarget

            Sequencer attackTargetSequencer = new Sequencer();
            TaskMoveToTarget taskMoveToTarget = new TaskMoveToTarget(this, "Target", 2.0f);
            TaskRotateTowardsTarget taskRotateTowardsTarget = new TaskRotateTowardsTarget(this, "Target", 10f);
            TaskAttackTarget taskAttackTarget = new TaskAttackTarget(this, "Target");

            attackTargetSequencer.AddChild(taskMoveToTarget);
            attackTargetSequencer.AddChild(taskRotateTowardsTarget);
            attackTargetSequencer.AddChild(taskAttackTarget);

            BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                attackTargetSequencer, "Target",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            rootSelector.AddChild(attackTargetDecorator);

            #endregion attackTarget

            #region heckLastSeenLocation

            Sequencer checkLastSeenLocationSequencer = new Sequencer();
            TaskMoveToLocation moveToLastSeenLocation = new TaskMoveToLocation(this, "LastSeenLocation", 1f);
            TaskWait waitLastSeenLocation = new TaskWait(7f);
            TaskRemoveBlackboardData removeLastLocation = new TaskRemoveBlackboardData(Blackboard, "LastSeenLocation");

            checkLastSeenLocationSequencer.AddChild(moveToLastSeenLocation);
            checkLastSeenLocationSequencer.AddChild(waitLastSeenLocation);
            checkLastSeenLocationSequencer.AddChild(removeLastLocation);

            BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(this,
                checkLastSeenLocationSequencer, "LastSeenLocation",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.none);

            rootSelector.AddChild(checkLastSeenLocationDecorator);

            #endregion heckLastSeenLocation

            #region patrol

            Sequencer patrolSequencer = new Sequencer();

            TaskGetNextPatrolPoint taskGetNextPatrolPoint = new TaskGetNextPatrolPoint(this, "PointPatrol");
            TaskMoveToLocation taskMoveToLocation = new TaskMoveToLocation(this, "PointPatrol");
            TaskWait taskWait = new TaskWait(2f);

            patrolSequencer.AddChild(taskGetNextPatrolPoint);
            patrolSequencer.AddChild(taskMoveToLocation);
            patrolSequencer.AddChild(taskWait);

            rootSelector.AddChild(patrolSequencer);

            #endregion patrol

            rootNode = rootSelector;
        }
    }
}