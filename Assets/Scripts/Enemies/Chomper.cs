using MonsterExterminator.Damage;
using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Chomper : Enemy
    {
        [SerializeField] TriggerDamageComponent damageComponent;

        private static readonly int Attack = Animator.StringToHash("attack");

        protected override void Start()
        {
            base.Start();
            damageComponent.SetTeamInterface(this);
        }

        public override void AttackTarget(Transform target)
        {
            Animator.SetTrigger(Attack);
        }

        public void AnimatorAttackPoint()
        {
            damageComponent.SetDamageEnabled(true);
        }

        public void AnimatorAttackEnd()
        {
            damageComponent.SetDamageEnabled(false);
        }
    }
}