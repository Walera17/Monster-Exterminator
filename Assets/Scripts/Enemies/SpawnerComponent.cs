﻿using UnityEngine;

namespace MonsterExterminator.Enemies
{
    public class SpawnerComponent : MonoBehaviour
    {
        [SerializeField] Transform spawnTransform;
        [SerializeField] private Spawner spawner;
        [SerializeField] Animator animator;
        [SerializeField] GameObject[] objectsToSpawn;

        private static readonly int Spawn = Animator.StringToHash("spawn");

        public bool StartSpawn()
        {
            if (objectsToSpawn.Length == 0) return false;

            if (animator != null)
                animator.SetTrigger(Spawn);
            else
                AnimatorSpawnImpl();

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