namespace MonsterExterminator.Common.BehaviorTree
{
    public enum NodeResult
    {
        Success,
        Failure,
        Inprogress
    }

    public enum RunCondition
    {
        KeyExists,
        KeyNotExists
    }

    public enum NotifyRule
    {
        RunConditionChange,
        KeyValueChange
    }

    public enum NotifyAbort
    {
        none,
        self,
        lower,
        both
    }
}