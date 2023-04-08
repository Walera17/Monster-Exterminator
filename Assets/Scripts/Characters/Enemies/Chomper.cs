using Characters.Damage;
using UnityEngine;

namespace Characters.Enemies
{
    public class Chomper : Enemy
    {
        [SerializeField] TriggerDamageComponent damageComponent;

        void Start()
        {
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