namespace MonsterExterminator.AI.BehaviorTree
{
    public abstract class TaskGroup : Node
    {
        Node root;
        protected BehaviorTree tree;

        protected TaskGroup(BehaviorTree behaviorTree)
        {
            tree = behaviorTree;
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

        public override Node Get()
        {
            return root.Get();
        }
    }
}