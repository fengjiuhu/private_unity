using UnityEngine;
using UnityEngine.EventSystems;

namespace CoinPush
{
    /// <summary>
    /// Lightweight on-screen joystick to drive Input System Vector2.
    /// </summary>
    public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform handle;
        [SerializeField] private float radius = 80f;

        public Vector2 Direction { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - (Vector2)transform.position;
            delta = Vector2.ClampMagnitude(delta, radius);
            Direction = delta / radius;
            if (handle != null)
            {
                handle.anchoredPosition = delta;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Direction = Vector2.zero;
            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }
        }

        /// <summary>
        /// Allows other UI wrappers to feed input directly.
        /// </summary>
        public void SetInput(Vector2 value)
        {
            Direction = Vector2.ClampMagnitude(value, 1f);
            if (handle != null)
            {
                handle.anchoredPosition = Direction * radius;
            }
        }
    }
}
