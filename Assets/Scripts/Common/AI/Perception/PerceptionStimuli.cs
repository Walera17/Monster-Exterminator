using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public class PerceptionStimuli : MonoBehaviour
    {
        private void Start()
        {
            Sense.RegisterStimuli(this);
        }

        private void OnDestroy()
        {
            Sense.UnRegisterStimuli(this);
        }
    }
}