using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Projectile : MonoBehaviour, ITeamInterface
    {
        [SerializeField] Rigidbody body;
        [SerializeField] private float flightHeight;

        private int teamID = -1;
        public int GetTeamID() => teamID;

        public void Launch(GameObject instigator, Vector3 destination)
        {
            if (instigator.TryGetComponent(out ITeamInterface teamInterface))
                teamID = teamInterface.GetTeamID();
           
            float gravity = Physics.gravity.magnitude;
            float halfFlightTime = Mathf.Sqrt((flightHeight * 2.0f) / gravity);

            float upSpeed = halfFlightTime * gravity;
            Vector3 dist = destination - transform.position;
            dist.y = 0;
            float horizontalDist = dist.magnitude;
            float fmdSpeed = horizontalDist / (2f * halfFlightTime);
            Vector3 flightVelocity = Vector3.up * upSpeed + instigator.transform.forward * fmdSpeed;
            body.AddForce(flightVelocity, ForceMode.VelocityChange);
        }
    }
}