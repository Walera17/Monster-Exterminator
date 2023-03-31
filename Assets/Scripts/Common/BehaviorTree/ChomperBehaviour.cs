namespace MonsterExterminator.Common.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            //TaskLog taskLog = new TaskLog("Logging");
            //TaskAlwaysFall taskAlwaysFall = new TaskAlwaysFall();

            //Sequencer root = new Sequencer();
            ////Selector root = new Selector();
            //root.AddChild(taskAlwaysFall);
            //root.AddChild(taskLog);
            //root.AddChild(taskWait);
            //rootNode = root;


            Selector rootSelector = new Selector();
            Sequencer attackTargetSequencer = new Sequencer();
            TaskMoveToTarget taskMoveToTarget = new TaskMoveToTarget(this, "Target", 1.8f);
            attackTargetSequencer.AddChild(taskMoveToTarget);

            BlackboardDecorator attackTarget = new BlackboardDecorator(this, attackTargetSequencer, "Target",
                RunCondition.KeyExists, NotifyRule.RunConditionChange, NotifyAbort.both);

            rootSelector.AddChild(attackTarget);

            Sequencer patrolSequencer = new Sequencer();

            TaskGetNextPatrolPoint taskGetNextPatrolPoint = new TaskGetNextPatrolPoint(this, "PointPatrol");
            TaskMoveToPoint taskMoveToPoint = new TaskMoveToPoint(this, "PointPatrol", 0.25f);
            TaskWait taskWait = new TaskWait(2f);

            patrolSequencer.AddChild(taskGetNextPatrolPoint);
            patrolSequencer.AddChild(taskMoveToPoint);
            patrolSequencer.AddChild(taskWait);

            rootSelector.AddChild(patrolSequencer);

            rootNode = rootSelector;
        }
    }
}