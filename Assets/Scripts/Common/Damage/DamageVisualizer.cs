using MonsterExterminator.Health;
using UnityEngine;

namespace MonsterExterminator.Damage
{
    public class DamageVisualizer : MonoBehaviour
    {
        [SerializeField] private Renderer mesh;
        [SerializeField] private Color damageEmissionColor;
        [SerializeField] private float blinkSpeed = 20f;
        [SerializeField] private string emissionColorPropertyName = "_Addition";
        [SerializeField] HealthComponent healthComponent;
        [SerializeField] private Shaker shaker;

        private Color originalEmissionColor;

        private void Start()
        {
            originalEmissionColor = mesh.material.GetColor(emissionColorPropertyName);
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
        }

        private void HealthComponent_OnTakeDamage(float health, float maxHealth, float delta, GameObject instigator)
        {
            shaker.StartShake();
            Color currentEmissionColor = mesh.material.GetColor(emissionColorPropertyName);
            if (Mathf.Abs((currentEmissionColor - originalEmissionColor).grayscale) < 0.1f)
                mesh.material.SetColor(emissionColorPropertyName, damageEmissionColor);
        }

        private void Update()
        {
            Color currentEmissionColor = mesh.material.GetColor(emissionColorPropertyName);
            Color newEmissionColor = Color.Lerp(currentEmissionColor, originalEmissionColor, Time.deltaTime * blinkSpeed);
            mesh.material.SetColor(emissionColorPropertyName, newEmissionColor);
        }

        private void OnDestroy()
        {
            healthComponent.OnTakeDamage -= HealthComponent_OnTakeDamage;
        }
    }
}