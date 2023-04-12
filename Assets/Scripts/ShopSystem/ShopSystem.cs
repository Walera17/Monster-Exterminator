using UnityEngine;

namespace ShopSystem
{
    [CreateAssetMenu(menuName = "Shop/ShopSystem")]
    public class ShopSystem : ScriptableObject
    {
        [SerializeField] private ShopItem[] shopItems;

        public ShopItem[] ShopItems => shopItems;

        public bool TryPurchase(ShopItem item)
        {
            return false;
        }
    }
}