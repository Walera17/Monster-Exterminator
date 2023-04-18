using UnityEngine;

namespace Characters.Enemies
{
    public class SpawnerComponent : MonoBehaviour
    {
        [SerializeField] Transform spawnTransform;
        [SerializeField] Animator animator;
        [SerializeField] private AudioClip spawnClip;
        [SerializeField] private float volume = 1f;
        [SerializeField] GameObject[] objectsToSpawn;

        private static readonly int Spawn = Animator.StringToHash("spawn");

        public bool StartSpawn()
        {
            if (objectsToSpawn.Length == 0) return false;

            if (animator != null)
                animator.SetTrigger(Spawn);
            else
                AnimatorSpawnImpl();

            Vector3 pos = transform.position;
            GamePlayStatics.PlayAudioAtLocation(spawnClip, pos, volume);

            return true;
        }

        public void AnimatorSpawnImpl()
        {
            GameObject newSpawn = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnTransform.position, spawnTransform.rotation);

            if (newSpawn.TryGetComponent(out ISpawnInterface newSpawnInterface))
                newSpawnInterface.SpawnedBy(gameObject);
        }
    }
}