using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class BehaviorTree : MonoBehaviour
    {
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