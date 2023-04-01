using MonsterExterminator.AI.BehaviorTree;
using MonsterExterminator.AI.Perception;
using MonsterExterminator.Health;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Enemy : MonoBehaviour, IBehaviorTreeInterface
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] Animator animator;
        [SerializeField] PerceptionComponent perceptionComponent;
        [SerializeField] MovementComponent movementComponent;
        [SerializeField] BehaviorTree behaviorsTree;

        private static readonly int Dead = Animator.StringToHash("dead");
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Start()
        {
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
            healthComponent.OnDead += HealthComponent_OnDead;
            perceptionComponent.OnPerceptionTargetChanged += PerceptionComponent_OnPerceptionTargetChanged;
        }

        private void PerceptionComponent_OnPerceptionTargetChanged(Transform targetTransform, bool sensed)
        {
            if (sensed)
                behaviorsTree.Blackboard.SetOrAddData("Target", targetTransform);
            else
            {
                behaviorsTree.Blackboard.SetOrAddData("LastSeenLocation", targetTransform.position);
                behaviorsTree.Blackboard.RemoveBlackboardData("Target");
            }
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

            if (behaviorsTree != null && behaviorsTree.Blackboard.GetBlackboardData("Target", out Transform targetTransform))
            {
                Vector3 targetPos = targetTransform.position + Vector3.up;
                Gizmos.DrawWireSphere(targetPos, 0.7f);
                Gizmos.DrawLine(transform.position + Vector3.up, targetPos);
            }
        }

        public void RotateToward(Transform target, bool verticalAim = false)
        {
            Vector3 aimDir = target.position - transform.position;
            aimDir.y = verticalAim ? aimDir.y : 0;

            movementComponent.RotateToward(aimDir.normalized);
        }
    }
}