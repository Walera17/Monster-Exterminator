using UnityEngine;
using UnityEngine.EventSystems;

namespace MonsterExterminator.UI
{
    public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            print("OnDrag = " + eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}