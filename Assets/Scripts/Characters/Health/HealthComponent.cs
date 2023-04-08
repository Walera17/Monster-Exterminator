using UnityEngine;

namespace Characters.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] float health = 100;
        [SerializeField] float maxHealth = 100;

        public delegate void OnHealthChangeDelegate(float health, float maxHealth, float delta);
        public delegate void OnTakeDamageDelegate(GameObject instigator);

        public delegate void OnDeadDelegate();

        public event OnHealthChangeDelegate OnHealthChange;
        public event OnTakeDamageDelegate OnTakeDamage;
        public event OnDeadDelegate OnDead;

        public void BroadcastHealthValueImmediately()
        {
            OnHealthChange?.Invoke(health, maxHealth, 0);
        }

        public void ChangeHealth(float delta, GameObject instigator)
        {
            if (delta == 0 || health == 0) return;

            health += delta;

            if (delta < 0)
                OnTakeDamage?.Invoke(instigator);

            OnHealthChange?.Invoke(health, maxHealth, delta);

            if (health <= 0)
            {
                health = 0;
                OnDead?.Invoke();
            }
        }
    }
}