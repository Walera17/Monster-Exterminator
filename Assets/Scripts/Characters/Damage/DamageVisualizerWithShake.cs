using UnityEngine;

namespace Characters.Damage
{
    public class DamageVisualizerWithShake : DamageVisualizer
    {
        private Shaker shaker;

        public void Construct(Shaker shakerParam)
        {
            shaker = shakerParam;
        }

        protected override void HealthComponent_OnTakeDamage(GameObject instigator)
        {
            base.HealthComponent_OnTakeDamage(instigator);
            if (shaker != null)
                shaker.StartShake();
        }
    }
}