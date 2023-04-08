using System.Collections.Generic;

namespace AI.BehaviorTree
{
    public abstract class Compositor : Node
    {
        readonly LinkedList<Node> children = new();
        LinkedListNode<Node> currentChild;

        public Node GetCurrentChild => currentChild.Value;

        public void AddChild(Node node)
        {
            children.AddLast(node);
        }

        protected override NodeResult Execute()
        {
            if (children.Count == 0)
            {
                return NodeResult.Success;
            }

            currentChild = children.First;
            return NodeResult.Inprogress;
        }

        protected bool Next()
        {
            if (currentChild != children.Last)
            {
                currentChild = currentChild.Next;
                return true;
            }

            return false;
        }

        protected override void End()
        {
            if (currentChild == null) return;

            currentChild.Value.Abort();
            currentChild = null;
        }

        public override void SortPriority(ref int priorityCounter)
        {
            base.SortPriority(ref priorityCounter);

            foreach (Node child in children)
            {
                child.SortPriority(ref priorityCounter);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (Node child in children)
            {
                child.Initialize();
            }
        }

        public override Node Get()
        {
            if (currentChild == null)
                return children.Count != 0 ? children.First.Value.Get() : this;

            return currentChild.Value.Get();
        }
    }
}