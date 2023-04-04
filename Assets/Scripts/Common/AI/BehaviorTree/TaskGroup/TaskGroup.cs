namespace MonsterExterminator.AI.BehaviorTree
{
    public abstract class TaskGroup : Node
    {
        private Node root;
        private BehaviorTree behaviorTree;

        protected TaskGroup(BehaviorTree tree)
        {
            behaviorTree = tree;
        }

        protected abstract void ConstructTree(out Node root);

        protected override NodeResult Execute()
        {
            return NodeResult.Inprogress;
        }

        protected override NodeResult Update()
        {
            return root.UpdateNode();
        }

        protected override void End()
        {
            root.Abort();
            base.End();
        }

        public override void SortPriority(ref int priorityCounter)
        {
            base.SortPriority(ref priorityCounter);
            root.SortPriority(ref priorityCounter);
        }

        public override void Initialize()
        {
            base.Initialize();
            ConstructTree(out root);
        }
    }
}