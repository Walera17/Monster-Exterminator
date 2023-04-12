using UnityEngine;

namespace ShopSystem
{
    public interface IPurchaseListener
    {
        void HandlePurchase(GameObject purchase);
    }
}