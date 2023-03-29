using System.Collections.Generic;

namespace MonsterExterminator.Common.BehaviorTree
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
            currentChild = null;
        }
    }
}