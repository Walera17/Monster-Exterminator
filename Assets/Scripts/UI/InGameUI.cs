using Characters.Health;
using UnityEngine;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] HealthBar healthBarPrefab;
        [SerializeField] private Transform parentHealthBars;

        public void CreateHealthBar(Transform owner, HealthComponent healthComponent)
        {
            HealthBar healthBar = Instantiate(healthBarPrefab, parentHealthBars);
            healthBar.Init(owner, healthComponent);
        }
    }
}