using MonsterExterminator.Common;
using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string attackSlotTag;
        [SerializeField] private float attackRateMult = 1f;
        [SerializeField] private AnimatorOverrideController overrideController;

        private static readonly int AttackRateMult = Animator.StringToHash("attackRateMult");

        public abstract void Attack();

        public string GetAttachSlotTag()
        {
            return attackSlotTag;
        }

        public GameObject Owner { get; private set; }

        public void Init(GameObject owner)
        {
            Owner = owner;
            UnEquip();
        }

        public void Equip()
        {
            gameObject.SetActive(true);
            Animator animator = Owner.GetComponent<Animator>();
            animator.runtimeAnimatorController = overrideController;
            animator.SetFloat(AttackRateMult, attackRateMult);
        }

        public void UnEquip()
        {
            gameObject.SetActive(false);
        }

        public void DamageGameObject(GameObject obj, float amt)
        {
            if (obj.TryGetComponent(out HealthComponent health))
                health.ChangeHealth(-amt, Owner);
        }
    }
}