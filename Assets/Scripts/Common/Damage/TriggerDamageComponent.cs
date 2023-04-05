using MonsterExterminator.Health;
using UnityEngine;

namespace MonsterExterminator.Damage
{
    public class TriggerDamageComponent : DamageComponent
    {
        [SerializeField] private float damage;
        [SerializeField] private Collider trigger;
        [SerializeField] private bool startedEnabled;

        private void Start()
        {
            SetDamageEnabled(startedEnabled);
        }

        public void SetDamageEnabled(bool enabledParam)
        {
            trigger.enabled = enabledParam;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!ShouldDamage(other.gameObject)) return;

            if (other.TryGetComponent(out HealthComponent health))
                health.ChangeHealth(-damage, gameObject);
        }
    }
}