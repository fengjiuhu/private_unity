using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// UI for upgrading player stats with cost display hooks.
    /// </summary>
    public class UpgradePanelUI : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        [SerializeField] private Button pushButton;
        [SerializeField] private Button vacuumButton;
        [SerializeField] private PlayerUpgrader upgrader;

        private void Awake()
        {
            if (upgrader == null)
            {
                upgrader = FindObjectOfType<PlayerUpgrader>();
            }
        }

        private void OnEnable()
        {
            if (speedButton != null) speedButton.onClick.AddListener(upgrader.UpgradeSpeed);
            if (pushButton != null) pushButton.onClick.AddListener(upgrader.UpgradePush);
            if (vacuumButton != null) vacuumButton.onClick.AddListener(upgrader.UpgradeVacuum);
        }

        private void OnDisable()
        {
            if (speedButton != null) speedButton.onClick.RemoveListener(upgrader.UpgradeSpeed);
            if (pushButton != null) pushButton.onClick.RemoveListener(upgrader.UpgradePush);
            if (vacuumButton != null) vacuumButton.onClick.RemoveListener(upgrader.UpgradeVacuum);
        }
    }
}
