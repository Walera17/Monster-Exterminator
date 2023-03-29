namespace MonsterExterminator.Common.BehaviorTree
{
    public abstract class Node
    {
        private bool started;

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
            {
                EndNode();
            }

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
            // clean up - приводить в порядок
        }

        private void EndNode()
        {
            started = false;
            End();
        }
    }
}