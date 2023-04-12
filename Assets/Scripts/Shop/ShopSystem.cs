using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Shop/ShopSystem")]
    public class ShopSystem : ScriptableObject
    {
        [SerializeField] private ShopItem[] shopItems;

        public ShopItem[] ShopItems => shopItems;

        public bool TryPurchase(ShopItem selectedItem, CreditComponent purchaser) => 
            purchaser.Purchase(selectedItem.price, selectedItem.item);
    }
}