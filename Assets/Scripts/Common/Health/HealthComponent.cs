using UnityEngine;

namespace MonsterExterminator.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] float health = 100;
        [SerializeField] float maxHealth = 100;

        public delegate void OnHealthChangeDelegate(float health, float maxHealth, float delta, GameObject instigator);

        public delegate void OnDeadDelegate();

        public event OnHealthChangeDelegate OnHealthChange, OnTakeDamage;
        public event OnDeadDelegate OnDead;

        public void BroadcastHealthValueImmediately()
        {
            OnHealthChange?.Invoke(health, maxHealth, 0, null);
        }

        public void ChangeHealth(float delta, GameObject instigator)
        {
            if (delta == 0 || health == 0) return;

            health += delta;

            if (delta < 0)
                OnTakeDamage?.Invoke(health, maxHealth, delta, instigator);

            OnHealthChange?.Invoke(health, maxHealth, delta, instigator);

            if (health <= 0)
            {
                health = 0;
                OnDead?.Invoke();
            }
        }
    }
}