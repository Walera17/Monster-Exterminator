using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        public Blackboard Blackboard { get; } = new();

        private Node rootNode;

        private void Start()
        {
            ConstructTree(out rootNode);
            SortTree();
        }

        private void SortTree()
        {
            int priorityCounter = 0;
            rootNode.SortPriority(ref priorityCounter);
        }

        protected abstract void ConstructTree(out Node rootNode);

        //private Node prevNode;

        //void ShowInConsole()
        //{
        //    Node currentNode = rootNode.Get();
        //    if (currentNode != null && prevNode != currentNode)
        //    {
        //        prevNode = currentNode;
        //        Debug.Log($"currentNode changed to = {currentNode}");
        //    }
        //}

        private void Update()
        {
            rootNode.UpdateNode();
            //ShowInConsole();
        }

        public void AbortLowerThan(int priority)
        {
            Node currentNode = rootNode.Get();
            if (currentNode.Priority > priority) 
                rootNode.Abort();
        }
    }
}