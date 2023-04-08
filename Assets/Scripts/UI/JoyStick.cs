using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform stickTransform;
        [SerializeField] RectTransform backgroundTransform;
        [SerializeField] RectTransform centerTransform;

        public delegate void OnStickInputValueUpdate(Vector2 value);
        public delegate void OnStickTaped();

        public event OnStickInputValueUpdate OnStickInputValueChanged;
        public event OnStickTaped OnTaped;

        private float maxOffset;
        private bool isDragging;

        private void Start()
        {
            maxOffset = backgroundTransform.sizeDelta.x / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 centerPosition = backgroundTransform.position;
                
            Vector2 localOffset = Vector2.ClampMagnitude(touchPosition - centerPosition, maxOffset);
            Vector2 inputValue = localOffset / maxOffset;

            stickTransform.position = centerPosition + localOffset;

            OnStickInputValueChanged?.Invoke(inputValue);
            isDragging = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            backgroundTransform.position = eventData.position;
            stickTransform.position = eventData.position;
            isDragging = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            backgroundTransform.position = centerTransform.position;
            stickTransform.position = backgroundTransform.position;

            OnStickInputValueChanged?.Invoke(Vector2.zero);

            if (!isDragging) 
                OnTaped?.Invoke();
        }
    }
}