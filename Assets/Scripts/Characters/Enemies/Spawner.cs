using UnityEngine;

namespace Characters.Enemies
{
    public class Spawner : Enemy
    {
        [SerializeField] private SpawnerComponent spawnerComponent;
        [SerializeField] private VFXSpec[] deathVFX;

        public override bool StartSpawn()
        {
            return spawnerComponent.StartSpawn();
        }

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