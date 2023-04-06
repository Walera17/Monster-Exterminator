using MonsterExterminator.AI.BehaviorTree;
using MonsterExterminator.AI.Perception;
using MonsterExterminator.Health;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public abstract class Enemy : MonoBehaviour, IBehaviorTreeInterface, ITeamInterface
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] Animator animator;
        [SerializeField] PerceptionComponent perceptionComponent;
        [SerializeField] MovementComponent movementComponent;
        [SerializeField] BehaviorTree behaviorsTree;
        [SerializeField] TeamRelation teamRelation;

        private float speed;
        private Vector3 prevPosition;
        private static readonly int Dead = Animator.StringToHash("dead");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Speed = Animator.StringToHash("speed");
        protected static readonly int Attack = Animator.StringToHash("attack");

        public int GetTeamID() => (int)teamRelation;

        public Animator Animator => animator;

        protected virtual void Start()
        {
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
            healthComponent.OnDead += HealthComponent_OnDead;
            perceptionComponent.OnPerceptionTargetChanged += PerceptionComponent_OnPerceptionTargetChanged;
            prevPosition = transform.position;
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

        private void Update()
        {
            CalculateSpeed();
        }

        private void CalculateSpeed()
        {
            if (movementComponent == null) return;

            Vector3 posDelta = transform.position - prevPosition;
            float deltaMagnitude = posDelta.magnitude / Time.deltaTime;

            speed = Mathf.Lerp(speed, deltaMagnitude, 5f * Time.deltaTime);

            animator.SetFloat(Speed, speed > 0.1f ? speed : 0);

            prevPosition = transform.position;
        }

        private void HealthComponent_OnDead()
        {
            TriggerDeathAnimation();
        }

        private void TriggerDeathAnimation()
        {
            animator.SetTrigger(Dead);
        }

        public void PlayStep()
        {
        }

        public void AnimatorDestroyGameObject()
        {
            Destroy(gameObject);
        }

        private void HealthComponent_OnTakeDamage(float health, float maxHealth, float delta, GameObject instigator)
        {
            animator.SetTrigger(Hit);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (behaviorsTree != null &&
                behaviorsTree.Blackboard.GetBlackboardData("Target", out Transform targetTransform))
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

        public virtual void AttackTarget(Transform target)
        {
        }
    }
}