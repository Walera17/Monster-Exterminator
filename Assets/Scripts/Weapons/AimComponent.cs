using UnityEngine;

namespace Weapons
{
    public class AimComponent : MonoBehaviour
    {
        [SerializeField] private Transform muzzle;
        [SerializeField] private float aimRange = 20f;
        [SerializeField] private LayerMask aimMask;

        public GameObject GetAimTarget(out Vector3 aimDir)
        {
            Vector3 aimStart = muzzle.position;
            aimDir = GetAimDir();
            if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
                return hitInfo.collider.gameObject;

            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(muzzle.position, muzzle.position + GetAimDir() * aimRange);
        }

        Vector3 GetAimDir()
        {
            Vector3 muzzleDir = muzzle.forward;
            return new Vector3(muzzleDir.x, 0, muzzleDir.z).normalized;
        }
    }
}