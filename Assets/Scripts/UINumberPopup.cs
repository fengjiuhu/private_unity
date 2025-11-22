using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Simple numeric popup animation for earnings.
    /// </summary>
    public class UINumberPopup : MonoBehaviour
    {
        [SerializeField] private Text label;
        [SerializeField] private float lifetime = 1f;
        private float timer;

        private void OnEnable()
        {
            timer = 0f;
        }

        public void SetValue(string value)
        {
            if (label != null)
            {
                label.text = value;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= lifetime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
