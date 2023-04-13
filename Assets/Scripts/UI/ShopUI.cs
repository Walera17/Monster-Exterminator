using Shop;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] ShopItemUI shopItemUIPrefab;
        [SerializeField] private RectTransform content;

        ShopSystem shopSystem;
        private CreditComponent creditComponent;
        readonly List<ShopItemUI> shopItems = new();

        public void Init(ShopSystem system, CreditComponent creditComp)
        {
            shopSystem = system;
            creditComponent = creditComp;
            InitShopItems();
        }

        public void RefreshShop()
        {
            foreach (ShopItemUI shopItemUI in shopItems)
                shopItemUI.Refresh(creditComponent.Credit);
        }

        private void InitShopItems()
        {
            foreach (ShopItem shopItem in shopSystem.ShopItems)
                AddShopItem(shopItem);

            RefreshShop();
        }

        private void AddShopItem(ShopItem shopItem)
        {
            ShopItemUI shopItemUI = Instantiate(shopItemUIPrefab, content);
            shopItemUI.Init(shopItem);
            shopItems.Add(shopItemUI);
        }
    }
}