using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string attachSlotTag;
        [SerializeField] private AnimatorOverrideController overrideController;

        public string GetAttachSlotTag()
        {
            return attachSlotTag;
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
            Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
        }

        public void UnEquip()
        {
            gameObject.SetActive(false);
        }
    }
}