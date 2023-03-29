namespace MonsterExterminator.Common.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            rootNode = new TaskWaitNode(2f);
        }
    }
}