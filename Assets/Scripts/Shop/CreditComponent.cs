using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class CreditComponent : MonoBehaviour
    {
        [SerializeField] private int credit;
        [SerializeField] private Component[] purchaseListenerComponents;

        readonly List<IPurchaseListener> purchaseListeners = new();
        public int Credit => credit;

        public delegate void OnCreditChangedDelegate(int newCredit);

        public event OnCreditChangedDelegate OnCreditChanged;

        private void Start()
        {
            foreach (Component component in purchaseListenerComponents)
            {
                if (component != null && component is IPurchaseListener listener)
                    purchaseListeners.Add(listener);
            }
        }

        public bool Purchase(int price, Object item)
        {
            if (credit < price) return false;

            credit -= price;

            OnCreditChanged?.Invoke(credit);

            BroadcastPurchase(item);

            return true;
        }

        void BroadcastPurchase(Object item)
        {
            foreach (IPurchaseListener listener in purchaseListeners)
            {
                if (listener.HandlePurchase(item))
                    return;
            }
        }
    }
}