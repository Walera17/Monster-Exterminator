using MonsterExterminator.Common;
using MonsterExterminator.Common.AI.Perception;
using MonsterExterminator.Common.BehaviorTree;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] Animator animator;
        [SerializeField] PerceptionComponent perceptionComponent;
        [SerializeField] BehaviorTree behaviorsTree;

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
            if (sensed)
                behaviorsTree.Blackboard.SetOrAddData("Target", targetStimuli);
            else
                behaviorsTree.Blackboard.RemoveBlackboardData("Target");
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

            if (behaviorsTree!= null && behaviorsTree.Blackboard.GetBlackboardData("Target", out PerceptionStimuli target))
            {
                Vector3 targetPos = target.transform.position + Vector3.up;
                Gizmos.DrawWireSphere(targetPos, 0.7f);
                Gizmos.DrawLine(transform.position + Vector3.up, targetPos);
            }
        }
    }
}