namespace AI.BehaviorTree
{
    public class PatrolTaskGroup : TaskGroup
    {
        private readonly float moveAcceptableDistance;
        public PatrolTaskGroup(BehaviorTree behaviorTree, float moveAcceptableDistance = 0.25f) : base(behaviorTree)
        {
            this.moveAcceptableDistance = moveAcceptableDistance;
        }

        protected override void ConstructTree(out Node root)
        {
            Sequencer patrolSequencer = new Sequencer();

            TaskGetNextPatrolPoint taskGetNextPatrolPoint = new TaskGetNextPatrolPoint(tree, "PointPatrol");
            TaskMoveToLocation taskMoveToLocation = new TaskMoveToLocation(tree, "PointPatrol", moveAcceptableDistance);
            TaskWait taskWait = new TaskWait(2f);

            patrolSequencer.AddChild(taskGetNextPatrolPoint);
            patrolSequencer.AddChild(taskMoveToLocation);
            patrolSequencer.AddChild(taskWait);

            root = patrolSequencer;
        }
    }
}