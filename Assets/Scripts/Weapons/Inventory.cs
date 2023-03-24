using System.Collections.Generic;
using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] Weapon[] initWeaponsPrefabs;
        [SerializeField] private Transform[] weaponSlots;
        [SerializeField] private Transform defaultWeaponSlot;

        List<Weapon> weapons;
        private Weapon currentWeapon;
        private int currentWeaponIndex = -1;

        private void Start()
        {
            InitializeWeapons();
        }

        private void InitializeWeapons()
        {
            weapons = new List<Weapon>();
            foreach (Weapon weapon in initWeaponsPrefabs)
            {
                Transform weaponSlot = defaultWeaponSlot;
                foreach (Transform slot in weaponSlots)
                {
                    if (slot.CompareTag(weapon.GetAttachSlotTag()))
                        weaponSlot = slot;
                }

                Weapon newWeapon = Instantiate(weapon, weaponSlot);
                newWeapon.Init(gameObject);
                weapons.Add(newWeapon);
            }

            NextWeapon();
        }

        public void NextWeapon()
        {
            int nextWeaponIndex = currentWeaponIndex + 1;
            if (nextWeaponIndex >= weapons.Count)
                nextWeaponIndex = 0;

            EquipWeapon(nextWeaponIndex);
        }

        private void EquipWeapon(int nextWeaponIndex)
        {
            if (nextWeaponIndex >= weapons.Count || nextWeaponIndex < 0) return;

            if (currentWeapon != null)
                currentWeapon.UnEquip();

            currentWeapon = weapons[nextWeaponIndex];
            currentWeaponIndex = nextWeaponIndex;
            currentWeapon.Equip();
        }
    }
}