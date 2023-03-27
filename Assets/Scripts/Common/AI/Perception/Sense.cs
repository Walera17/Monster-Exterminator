using System.Collections.Generic;
using UnityEngine;

namespace MonsterExterminator.Common.AI.Perception
{
    public abstract class Sense : MonoBehaviour
    {
        static readonly List<PerceptionStimuli> registerStimuliList = new();            // зарегистрированные стимулы
        readonly List<PerceptionStimuli> perceivableStimuliList = new();                // воспринимаемые стимулы

        public static void RegisterStimuli(PerceptionStimuli stimuli)
        {
            if (!registerStimuliList.Contains(stimuli))
                registerStimuliList.Add(stimuli);
        }

        public static void UnRegisterStimuli(PerceptionStimuli stimuli)
        {
            if (registerStimuliList.Contains(stimuli))
                registerStimuliList.Remove(stimuli);
        }

        protected abstract bool InStimuliSensible(PerceptionStimuli stimuli);

        private void Update()
        {
            foreach (PerceptionStimuli stimuli in registerStimuliList)
            {
                if (InStimuliSensible(stimuli))
                {
                    if (!perceivableStimuliList.Contains(stimuli))
                    {
                        perceivableStimuliList.Add(stimuli);
                        print($"Add STIMULI {stimuli.name} To {this} {name}");
                    }
                }
                else if (perceivableStimuliList.Contains(stimuli))
                {
                    perceivableStimuliList.Remove(stimuli);
                    print($"Remove STIMULI {stimuli.name} From {this} {name}");
                }
            }
        }

        private void OnDrawGizmos()
        {
            DrawDebug();
        }

        protected virtual void DrawDebug()
        {
        }
    }
}