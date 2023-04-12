using UnityEngine;

namespace Shop
{
    public interface IPurchaseListener
    {
        bool HandlePurchase(Object purchaseItem);
    }
}