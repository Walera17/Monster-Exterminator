using UnityEngine;

namespace Characters.AbilitySystem
{
    public class Ability : ScriptableObject
    {
        AbilityComponent abilityComponent;

        public void Init(AbilityComponent component)
        {
            abilityComponent = component;
        }
    }
}