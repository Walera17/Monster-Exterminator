namespace MonsterExterminator.Common.BehaviorTree
{
    public class Selector : Compositor
    {
        protected override NodeResult Update()
        {
            NodeResult result = GetCurrentChild.UpdateNode();

            return result switch
            {
                NodeResult.Success => NodeResult.Success,
                NodeResult.Failure => Next() ? NodeResult.Inprogress : NodeResult.Failure,
                _ => NodeResult.Inprogress
            };
        }
    }
}