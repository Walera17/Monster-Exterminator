using MonsterExterminator.Common;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterExterminator.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider slider;

        private Transform attachPoint;
        Camera cam;
        HealthComponent _healthComponent;

        private void Start()
        {
            cam = Camera.main;
        }

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

        public void SetHealthValue(float health, float maxHealth)
        {
            slider.value = health / maxHealth;
        }

        private void Update()
        {
            transform.position = cam.WorldToScreenPoint(attachPoint.position);
        }
    }
}