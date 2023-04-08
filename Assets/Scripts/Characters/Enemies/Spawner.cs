using UnityEngine;

namespace Characters.Enemies
{
    public class Spawner : Enemy
    {
        [SerializeField] private VFXSpec[] deathVFX;

        protected override void DeadEnemy()
        {
            foreach (VFXSpec spec in deathVFX)
            {
                ParticleSystem particle = Instantiate(spec.particleSystem);
                particle.transform.position = transform.position;
                particle.transform.localScale = Vector3.one * spec.size;
            }
        }
    }
}