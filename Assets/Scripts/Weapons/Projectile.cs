using MonsterExterminator.Characters;
using MonsterExterminator.Characters.Damage;
using MonsterExterminator.Characters.Enemies;
using UnityEngine;

namespace MonsterExterminator.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] DamageComponent damageComponent;
        [SerializeField] Rigidbody body;
        [SerializeField] private float flightHeight;
        [SerializeField] private ParticleSystem explosionVFX;

        private ITeamInterface instigatorInterface;

        public void Launch(ITeamInterface instigator, Transform target)
        {
            instigatorInterface = instigator;
            damageComponent.SetTeamInterface(instigator);

            body.AddForce(GetFlightVelocity(target), ForceMode.VelocityChange);
        }

        private Vector3 GetFlightVelocity(Transform target)
        {
            float gravity = Physics.gravity.magnitude;
            float halfFlightTime = Mathf.Sqrt((flightHeight * 2.0f) / gravity);

            float upSpeed = halfFlightTime * gravity;
            Vector3 distance = target.position - transform.position;
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
            Vector3 position = transform.position;
            Instantiate(explosionVFX, position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}