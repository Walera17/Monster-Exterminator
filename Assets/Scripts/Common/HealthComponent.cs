using UnityEngine;

namespace MonsterExterminator.Common
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] float health = 100;
        [SerializeField] float maxHealth = 100;

        public delegate void OnHealthChangeDelegate(float health, float maxHealth, GameObject instigator);
        public delegate void OnDeadDelegate();

        public event OnHealthChangeDelegate OnHealthChange, OnTakeDamage;
        public event OnDeadDelegate OnDead;

        public void ChangeHealth(float delta, GameObject instigator)
        {
            if (delta == 0 || health == 0) return;

            health += delta;

            if (delta < 0)
                OnTakeDamage?.Invoke(health, maxHealth, instigator);

            OnHealthChange?.Invoke(health, maxHealth, instigator);

            if (health <= 0)
            {
                health = 0;
                OnDead?.Invoke();
            }
        }
    }
}