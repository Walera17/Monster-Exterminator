using UnityEngine;

namespace MonsterExterminator.Common
{
    public class HealthComponent : MonoBehaviour
    {
        public delegate void OnHealthChangeDelegate(float health,float maxHealth);
        public delegate void OnDeadDelegate();

        public event OnHealthChangeDelegate OnHealthChange, OnTakeDamage;
        public event OnDeadDelegate OnDead;

        [SerializeField] float health = 100;
        [SerializeField] float maxHealth = 100;

        public void ChangeHealth(float delta)
        {
            if (delta == 0 || health == 0) return;

            health += delta;

            if (delta < 0)
                OnTakeDamage?.Invoke(health,  maxHealth);

            OnHealthChange?.Invoke(health,  maxHealth);

            if (health <= 0)
            {
                health = 0;
                OnDead?.Invoke();
            }
        }
    }
}