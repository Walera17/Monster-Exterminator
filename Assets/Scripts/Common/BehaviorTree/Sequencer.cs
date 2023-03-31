namespace MonsterExterminator.Common.BehaviorTree
{
    public class Sequencer : Compositor
    {
        protected override NodeResult Update()
        {
            NodeResult result = GetCurrentChild.UpdateNode();

            return result switch
            {
                NodeResult.Failure => NodeResult.Failure,
                NodeResult.Success => Next() ? NodeResult.Inprogress : NodeResult.Success,
                _ => NodeResult.Inprogress
            };
        }

        public override string ToString() => GetType().Name;
    }
}