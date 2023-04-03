using MonsterExterminator.Health;
using UnityEngine;

namespace MonsterExterminator.Damage
{
    public class DamageVisualizer : MonoBehaviour
    {
        [SerializeField] private Renderer mesh;
        [SerializeField] private Color damageEmissionColor;
        [SerializeField] private float blinkSpeed = 2.0f;
        [SerializeField] private string emissionColorPropertyName = "_Addition";
        [SerializeField] HealthComponent healthComponent;

        private Color originalEmissionColor;

        private void Start()
        {
            Material material = mesh.material;
            mesh.material = new Material(material);

            originalEmissionColor = mesh.material.GetColor(emissionColorPropertyName);
            healthComponent.OnTakeDamage += HealthComponent_OnTakeDamage;
        }

        private void HealthComponent_OnTakeDamage(float health, float maxHealth, float delta, GameObject instigator)
        {
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
    }
}