using MonsterExterminator.Common;
using MonsterExterminator.Common.AI.Perception;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] Animator animator;
        [SerializeField] PerceptionComponent perceptionComponent;

        private PerceptionStimuli target;

        private static readonly int Dead = Animator.StringToHash("dead");
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Start()
        {
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
            healthComponent.OnDead += HealthComponent_OnDead;
            perceptionComponent.OnPerceptionTargetChanged += PerceptionComponent_OnPerceptionTargetChanged;
        }

        private void PerceptionComponent_OnPerceptionTargetChanged(PerceptionStimuli targetStimuli, bool sensed)
        {
            target = sensed ? targetStimuli : null;
        }

        private void OnDestroy()
        {
            healthComponent.OnTakeDamage -= HealthComponent_OnTakeDamage;
            healthComponent.OnDead -= HealthComponent_OnDead;
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

        private void HealthComponent_OnTakeDamage(float health, float maxHealth, GameObject instigator)
        {
            animator.SetTrigger(Hit);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (target == null) return;

            Vector3 targetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(targetPos, 0.7f);
            Gizmos.DrawLine(transform.position + Vector3.up, targetPos);
        }
    }
}