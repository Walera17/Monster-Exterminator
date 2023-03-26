using MonsterExterminator.Common;
using MonsterExterminator.UI;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] Animator animator;
        [SerializeField] private Transform healthBarAttachPoint;

        private static readonly int Dead = Animator.StringToHash("dead");
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Start()
        {
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
            healthComponent.OnDead += HealthComponent_OnDead;
            CreateHealthBar();
        }

        private void OnDestroy()
        {
            healthComponent.OnTakeDamage -= HealthComponent_OnTakeDamage;
            healthComponent.OnDead -= HealthComponent_OnDead;
        }

        private void CreateHealthBar()
        {
            FindObjectOfType<InGameUI>().CreateHealthBar(healthBarAttachPoint, healthComponent);
        }

        private void HealthComponent_OnDead()
        {
            TriggerDeathAnimation();
        }

        private void TriggerDeathAnimation()
        {
            animator.SetTrigger(Dead);
        }

        public void AnimatorDestroyGameObject()
        {
            Destroy(gameObject);
        }

        private void HealthComponent_OnTakeDamage(float health, float maxHealth)
        {
            animator.SetTrigger(Hit);
        }
    }
}