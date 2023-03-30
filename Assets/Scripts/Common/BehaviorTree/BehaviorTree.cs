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

        private void Update()
        {
            rootNode.UpdateNode();
        }
    }
}