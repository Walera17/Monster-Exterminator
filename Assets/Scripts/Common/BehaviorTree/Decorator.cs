namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class Decorator : Node
    {
        public Node Child { get; }

        protected Decorator(Node child)
        {
            Child = child;
        }
    }
}