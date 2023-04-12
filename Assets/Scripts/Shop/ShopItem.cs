using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Shop/ShopItem")]
    public class ShopItem : ScriptableObject
    {
        public string title;
        public Sprite icon;
        public int price;
        public Object item;
        [TextArea(3, 10)] public string description;
    }
}