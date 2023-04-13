using Shop;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] ShopItemUI shopItemUIPrefab;
        [SerializeField] private RectTransform content;
        [SerializeField] private TMP_Text creditText;
        [SerializeField] private Button backButton;
        [SerializeField] private Button buyButton;

        private UIManager uiManager;
        ShopSystem shopSystem;
        private CreditComponent creditComponent;
        readonly List<ShopItemUI> shopItems = new();

        public void Init(UIManager manager, ShopSystem system, CreditComponent creditComp)
        {
            uiManager = manager;
            shopSystem = system;
            creditComponent = creditComp;
            InitShopItems();
            backButton.onClick.AddListener(uiManager.SwitchToGamePlayControl);
            creditComponent.OnCreditChanged += RefreshShop;
        }

        private void OnDestroy()
        {
            creditComponent.OnCreditChanged -= RefreshShop;
        }

        public void RefreshShop(int credit)
        {
            creditText.text = credit.ToString();

            foreach (ShopItemUI shopItemUI in shopItems)
                shopItemUI.Refresh(credit);
        }

        private void InitShopItems()
        {
            foreach (ShopItem shopItem in shopSystem.ShopItems)
                AddShopItem(shopItem);

            RefreshShop(creditComponent.Credit);
        }

        private void AddShopItem(ShopItem shopItem)
        {
            ShopItemUI shopItemUI = Instantiate(shopItemUIPrefab, content);
            shopItemUI.Init(shopItem);
            shopItems.Add(shopItemUI);
        }
        
    }
}