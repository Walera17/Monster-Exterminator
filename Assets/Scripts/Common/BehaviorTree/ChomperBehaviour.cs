namespace MonsterExterminator.Common.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            Selector rootSelector = new Selector();

            #region attackTarget

            Sequencer attackTargetSequencer = new Sequencer();
            TaskMoveToTarget taskMoveToTarget = new TaskMoveToTarget(this, "Target", 1.8f);
            attackTargetSequencer.AddChild(taskMoveToTarget);

            BlackboardDecorator attackTarget = new BlackboardDecorator(this,
                attackTargetSequencer, "Target",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            rootSelector.AddChild(attackTarget);

            #endregion attackTarget

            #region heckLastSeenLocation

            Sequencer checkLastSeenLocationSequencer = new Sequencer();
            TaskMoveToPoint moveToLastSeenLocation = new TaskMoveToPoint(this, "LastSeenLocation", 3f);
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
            TaskMoveToPoint taskMoveToPoint = new TaskMoveToPoint(this, "PointPatrol");
            TaskWait taskWait = new TaskWait(2f);

            patrolSequencer.AddChild(taskGetNextPatrolPoint);
            patrolSequencer.AddChild(taskMoveToPoint);
            patrolSequencer.AddChild(taskWait);

            rootSelector.AddChild(patrolSequencer);

            #endregion patrol

            rootNode = rootSelector;
        }
    }
}