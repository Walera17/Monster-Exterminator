namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class Decorator : Node
    {
        public Node Child { get; }

        protected Decorator(Node child)
        {
            Child = child;
        }

        public override void SortPriority(ref int priorityCounter)
        {
            base.SortPriority(ref priorityCounter);
            Child.SortPriority(ref priorityCounter);
        }
    }
}