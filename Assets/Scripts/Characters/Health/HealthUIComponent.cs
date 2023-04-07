using MonsterExterminator.UI;
using UnityEngine;

namespace MonsterExterminator.Characters.Health
{
    public class HealthUIComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private Transform healthBarAttachPoint;

        private void Start()
        {
            CreateHealthBar();
        }
        private void CreateHealthBar()
        {
            FindObjectOfType<InGameUI>().CreateHealthBar(healthBarAttachPoint, healthComponent);
        }
    }
}