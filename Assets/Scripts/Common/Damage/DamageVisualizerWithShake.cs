using UnityEngine;

namespace MonsterExterminator.Damage
{
    public class DamageVisualizerWithShake : DamageVisualizer
    {
        private Shaker shaker;

        public void Construct(Shaker shakerParam)
        {
            shaker = shakerParam;
        }

        protected override void HealthComponent_OnTakeDamage(float health, float maxHealth, float delta, GameObject instigator)
        {
            base.HealthComponent_OnTakeDamage(health, maxHealth, delta, instigator);
            if (shaker != null)
                shaker.StartShake();
        }
    }
}