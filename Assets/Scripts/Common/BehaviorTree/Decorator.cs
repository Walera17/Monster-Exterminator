namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class Decorator : Node
    {
        readonly Node child;

        public Node Child => child;

        protected Decorator(Node child)
        {
            this.child = child;
        }
    }
}