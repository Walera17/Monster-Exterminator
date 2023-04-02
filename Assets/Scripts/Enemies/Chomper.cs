using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class Chomper : Enemy
    {
        private static readonly int Attack = Animator.StringToHash("attack");

        public override void AttackTarget(Transform target)
        {
            Animator.SetTrigger(Attack);
        }
    }
}