using UnityEngine;

namespace MonsterExterminator.Enemies
{
    [System.Serializable]
    public class VFXSpec
    {
        public ParticleSystem particleSystem;
        public float size;
    }

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