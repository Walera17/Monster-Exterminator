namespace MonsterExterminator.Common.BehaviorTree
{
    public class ChomperBehaviour : BehaviorTree
    {
        protected override void ConstructTree(out Node rootNode)
        {
            TaskWait taskWait = new TaskWait(2f);
            TaskLog taskLog = new TaskLog("Logging");
            TaskAlwaysFall taskAlwaysFall = new TaskAlwaysFall();

            Sequencer root = new Sequencer();
            //Selector root = new Selector();
            root.AddChild(taskAlwaysFall);
            root.AddChild(taskLog);
            root.AddChild(taskWait);

            rootNode = root;
        }
    }
}