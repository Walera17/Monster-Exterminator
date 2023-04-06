namespace MonsterExterminator.AI.BehaviorTree
{
    public class SpatterBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            Selector rootSelector = new Selector();

            rootSelector.AddChild(new AttackTargetTaskGroup(this, 5, 4.5f));

            rootSelector.AddChild(new LastSeenLocationTaskGroup(this));

            rootSelector.AddChild(new PatrolTaskGroup(this));

            rootNode = rootSelector;
        }
    }
}