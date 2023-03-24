using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string attachSlotTag;

        public string GetAttachSlotTag()
        {
            return attachSlotTag;
        }

        public GameObject Owner { get; private set; }

        public void Init(GameObject owner)
        {
            Owner = owner;
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        public void UnEquip()
        {
            gameObject.SetActive(false);
        }
    }
}