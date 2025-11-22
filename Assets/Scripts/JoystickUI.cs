using UnityEngine;
using UnityEngine.EventSystems;

namespace CoinPush
{
    /// <summary>
    /// Basic on-screen joystick to feed direction to MobileJoystick.
    /// </summary>
    public class JoystickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform handle;
        [SerializeField] private float radius = 80f;
        [SerializeField] private MobileJoystick joystick;

        private Vector2 center;

        private void Awake()
        {
            joystick ??= FindObjectOfType<MobileJoystick>();
        }

        private void Start()
        {
            center = handle.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - (Vector2)handle.position;
            direction = Vector2.ClampMagnitude(direction, radius);
            handle.anchoredPosition = center + direction;
            joystick.SetInput(direction / radius);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            handle.anchoredPosition = center;
            joystick.SetInput(Vector2.zero);
        }
    }
}
