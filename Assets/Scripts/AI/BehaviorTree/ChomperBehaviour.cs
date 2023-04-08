namespace AI.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            Selector rootSelector = new Selector();

            rootSelector.AddChild(new AttackTargetTaskGroup(this));

            rootSelector.AddChild(new LastSeenLocationTaskGroup(this));

            rootSelector.AddChild(new PatrolTaskGroup(this));

            rootNode = rootSelector;
        }
    }
}