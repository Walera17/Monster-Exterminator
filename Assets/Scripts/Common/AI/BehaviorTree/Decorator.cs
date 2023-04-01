namespace MonsterExterminator.AI.BehaviorTree
{
    public abstract class Decorator : Node
    {
        public Node child { get; }

        protected Decorator(Node child)
        {   
            this.child = child;
        }

        public override void SortPriority(ref int priorityCounter)
        {
            base.SortPriority(ref priorityCounter);
            child.SortPriority(ref priorityCounter);
        }

        public override Node Get() => child.Get();
    }
}