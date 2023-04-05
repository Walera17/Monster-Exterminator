using MonsterExterminator.Damage;
using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] DamageComponent damageComponent;
        [SerializeField] Rigidbody body;
        [SerializeField] private float flightHeight;

        private ITeamInterface instigatorInterface;

        public void Launch(ITeamInterface instigator, Vector3 destination)
        {
            instigatorInterface = instigator;
            damageComponent.SetTeamInterface(instigator);

            body.AddForce(GetFlightVelocity(destination), ForceMode.VelocityChange);
        }

        private Vector3 GetFlightVelocity(Vector3 destination)
        {
            float gravity = Physics.gravity.magnitude;
            float halfFlightTime = Mathf.Sqrt((flightHeight * 2.0f) / gravity);

            float upSpeed = halfFlightTime * gravity;
            Vector3 distance = destination - transform.position;
            distance.y = 0;

            float fmdSpeed = distance.magnitude / (2f * halfFlightTime);
            Vector3 flightVelocity = Vector3.up * upSpeed + distance.normalized * fmdSpeed;

            return flightVelocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (instigatorInterface.GetRelationTowards(other.gameObject) != TeamRelation.Friendly)
                Explode();
        }

        private void Explode()
        {
            Destroy(gameObject);
        }
    }
}