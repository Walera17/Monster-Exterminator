namespace AI.BehaviorTree
{
    public abstract class Node
    {
        private bool started;
        private int priority;

        public int Priority => priority;

        public NodeResult UpdateNode()
        {
            // one off thing - одноразовая вещь
            if (!started)
            {
                started = true;
                NodeResult executeResult = Execute();
                if (executeResult != NodeResult.Inprogress)
                {
                    EndNode();
                    return executeResult;
                }
            }

            // time based - на основе времени
            NodeResult updateResult = Update();
            if (updateResult != NodeResult.Inprogress)
                EndNode();

            return updateResult;
        }

        // override child class - переопределить дочерний класс
        protected virtual NodeResult Execute()
        {
            // one off thing - одноразовая вещь
            return NodeResult.Success;
        }

        protected virtual NodeResult Update()
        {
            // time based - на основе времени
            return NodeResult.Success;
        }

        protected virtual void End()
        {
            // reset and clean up - приводить в порядок
        }

        private void EndNode()
        {
            started = false;
            End();
        }

        public virtual void Initialize() { }

        public void Abort() => EndNode();

        public virtual void SortPriority(ref int priorityCounter) => priority = priorityCounter++;

        public virtual Node Get() => this;
    }
}