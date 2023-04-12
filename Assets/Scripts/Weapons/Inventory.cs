using Shop;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Inventory : MonoBehaviour, IPurchaseListener
    {
        [SerializeField] Weapon[] initWeaponsPrefabs;
        [SerializeField] private Transform[] weaponSlots;
        [SerializeField] private Transform defaultWeaponSlot;

        List<Weapon> weapons;
        private Weapon currentWeapon;
        private int currentWeaponIndex = -1;

        public Weapon CurrentWeapon => currentWeapon;

        private void Start()
        {
            InitializeWeapons();
        }

        private void InitializeWeapons()
        {
            weapons = new List<Weapon>();
            foreach (Weapon weapon in initWeaponsPrefabs)
            {
                GiveNewWeapon(weapon);
            }

            NextWeapon();
        }

        private void GiveNewWeapon(Weapon weapon)
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

        public bool HandlePurchase(Object purchaseItem)
        {
            GameObject item = purchaseItem as GameObject;
            if (item == null) return false;

            Weapon weapon = item.GetComponent<Weapon>();
            if (weapon == null) return false;

            GiveNewWeapon(weapon);
            return true;
        }
    }
}