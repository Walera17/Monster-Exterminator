using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button button;
        [SerializeField] private Image grayOutCover;
        [SerializeField] private Color validateCredit;
        [SerializeField] private Color failedCredit;

        private ShopItem item;

        public void Init(ShopItem shopItem, int currentCredit)
        {
            item = shopItem;
            icon.sprite = shopItem.icon;
            titleText.text = shopItem.title;
            priceText.text = "$" + shopItem.price;
            descriptionText.text = shopItem.description;

            Refresh(currentCredit);
        }

        private void Refresh(int currentCredit)
        {
            if (currentCredit < item.price)
            {
                grayOutCover.enabled = true;
                priceText.color = failedCredit;
                button.interactable = false;
            }
            else
            {
                grayOutCover.enabled = false;
                priceText.color = validateCredit;
                button.interactable = true;
            }
        }
    }
}