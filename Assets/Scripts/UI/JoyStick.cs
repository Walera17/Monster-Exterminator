using UnityEngine;
using UnityEngine.EventSystems;

namespace MonsterExterminator.UI
{
    public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform stickTransform;
        [SerializeField] RectTransform backgroundTransform;
        [SerializeField] RectTransform centerTransform;

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 centerPosition = backgroundTransform.position;
            Vector2 localOffset = Vector2.ClampMagnitude(touchPosition - centerPosition, backgroundTransform.sizeDelta.x / 2);

            stickTransform.position = centerPosition + localOffset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            backgroundTransform.position = eventData.position;
            stickTransform.position= eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            backgroundTransform.position = centerTransform.position;
            stickTransform.position = backgroundTransform.position;
        }
    }
}