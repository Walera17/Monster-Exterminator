using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        readonly Blackboard blackboard = new();
        public Blackboard Blackboard => blackboard;

        private Node rootNode;

        private void Start()
        {
            ConstructTree(out rootNode);
        }

        protected abstract void ConstructTree(out Node rootNode);

        private void Update()
        {
            rootNode.UpdateNode();
        }
    }
}