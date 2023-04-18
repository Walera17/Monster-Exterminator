using Rewards;
using System.Collections;
using UnityEngine;

namespace Characters.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] AudioClip hitClip;
        [SerializeField] AudioClip deathClip;
        [SerializeField] private float volumeClip = 1f;
        [SerializeField] float maxHealth = 100;
        float health;

        public delegate void OnChangeParameterDelegate(float value, float maxValue, float delta);

        public delegate void OnTakeDamageDelegate(GameObject instigator);

        public event OnChangeParameterDelegate OnHealthChange;
        public event OnTakeDamageDelegate OnTakeDamage;
        public event OnTakeDamageDelegate OnDead;

        private void Awake()
        {
            health = maxHealth;
        }

        public void BroadcastHealthValueImmediately()
        {
            OnHealthChange?.Invoke(health, maxHealth, 0);
        }

        public void ChangeHealth(float delta, GameObject instigator)
        {
            if (delta == 0 || health == 0) return;

            health += delta;

            health = Mathf.Clamp(health, 0, maxHealth);

            if (delta < 0)
            {
                OnTakeDamage?.Invoke(instigator);
                Vector3 loc = transform.position;
                GamePlayStatics.PlayAudioAtLocation(hitClip, loc, volumeClip);
            }

            OnHealthChange?.Invoke(health, maxHealth, delta);

            if (health == 0)
            {
                OnDead?.Invoke(instigator);
                Vector3 loc = transform.position;
                GamePlayStatics.PlayAudioAtLocation(deathClip, loc, volumeClip);
            }
        }

        public void HealthRegenerate(float healthRegenerateAmount, float speedRegenerate)
        {
            if (speedRegenerate == 0)
            {
                health += healthRegenerateAmount;
                health = Mathf.Clamp(health, 0, maxHealth);
                BroadcastHealthValueImmediately();
                return;
            }

            StartCoroutine(HealthRegenerateCoroutine(healthRegenerateAmount, speedRegenerate));
        }

        private IEnumerator HealthRegenerateCoroutine(float healthRegenerateAmount, float speedRegenerate)
        {
            float deltaHealth = GetDeltaHealth(healthRegenerateAmount);

            while (deltaHealth > 0 && health < maxHealth)
            {
                deltaHealth -= speedRegenerate * Time.deltaTime;
                health += speedRegenerate * Time.deltaTime;
                BroadcastHealthValueImmediately();
                yield return null;
            }

            health = Mathf.FloorToInt(health);
        }

        public float GetDeltaHealth(float healthRegenerateAmount)
        {
            float deltaHealth = maxHealth - health;
            if (deltaHealth > healthRegenerateAmount)
                deltaHealth = healthRegenerateAmount;

            return deltaHealth;
        }

        public void Reward(Reward reward)
        {
            ChangeHealth(reward.health, null);
        }
    }
}