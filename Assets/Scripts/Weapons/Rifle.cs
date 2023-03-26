using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Rifle : Weapon
    {
        [Header("Specific Weapon")]
        [SerializeField] private AimComponent aimComponent;

        public override void Attack()
        {
            GameObject target = aimComponent.GetAimTarget();
            print($"target at : {target}");
        }
    }
}