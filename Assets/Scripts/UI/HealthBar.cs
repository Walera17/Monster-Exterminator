using Characters.Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider slider;

        private Transform attachPoint;
        HealthComponent _healthComponent;
        Camera cam;

        private void Start() 
            => cam = Camera.main;

        public void Init(Transform owner, HealthComponent healthComponent)
        {
            attachPoint = owner;
            _healthComponent = healthComponent;
            _healthComponent.OnHealthChange += SetHealthValue;
            _healthComponent.OnDead += HealthComponent_OnDead;
        }

        private void HealthComponent_OnDead()
        {
            _healthComponent.OnHealthChange -= SetHealthValue;
            _healthComponent.OnDead -= HealthComponent_OnDead;
            Destroy(gameObject);
        }

        public void SetHealthValue(float health, float maxHealth, float delta) 
            => slider.value = health / maxHealth;

        private void Update()
        => transform.position = cam.WorldToScreenPoint(attachPoint.position);
    }
}