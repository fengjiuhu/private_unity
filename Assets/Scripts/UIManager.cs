using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Updates HUD values and lightweight upgrade buttons.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Currency Texts")]
        [SerializeField] private Text goldText;
        [SerializeField] private Text potionText;
        [SerializeField] private Text gemText;

        [Header("Upgrade Buttons")]
        [SerializeField] private Button speedButton;
        [SerializeField] private Button forceButton;
        [SerializeField] private Button vacuumButton;

        [Header("Floating Text")]
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private FloatingTextController floatingTextController;

        [Header("Modular UI")]
        [SerializeField] private CurrencyUI currencyUI;
        [SerializeField] private LevelPanelUI levelPanelUI;
        [SerializeField] private UpgradePanelUI upgradePanelUI;
        [SerializeField] private JoystickUI joystickUI;
        [SerializeField] private SettingsUI settingsUI;
        [SerializeField] private RewardUI rewardUI;

        private CurrencyManager currency;
        private UpgradeSystem upgrades;
        private PlayerController player;

        public void Initialize(CurrencyManager currencyManager, UpgradeSystem upgradeSystem, PlayerController playerController)
        {
            currency = currencyManager;
            upgrades = upgradeSystem;
            player = playerController;

            HookCurrency();
            HookButtons();
        }

        public void ShowFloatingText(string content, Vector3 worldPosition)
        {
            if (floatingTextController != null)
            {
                floatingTextController.ShowValue(content);
                return;
            }

            if (floatingTextPrefab == null)
            {
                return;
            }

            GameObject textObj = Instantiate(floatingTextPrefab, transform);
            textObj.GetComponentInChildren<Text>().text = content;
        }

        private void HookCurrency()
        {
            if (goldText != null) currency.OnGoldChanged += value => goldText.text = value.ToString();
            if (potionText != null) currency.OnGreenPotionChanged += value => potionText.text = value.ToString();
            if (gemText != null) currency.OnRedGemChanged += value => gemText.text = value.ToString();
            if (currencyUI != null)
            {
                currencyUI.UpdateDisplay(currency.Gold, currency.GreenPotion, currency.RedGem);
            }
        }

        private void HookButtons()
        {
            if (speedButton != null)
            {
                speedButton.onClick.AddListener(() => upgrades.TryUpgradeSpeed());
            }

            if (forceButton != null)
            {
                forceButton.onClick.AddListener(() => upgrades.TryUpgradePushForce());
            }

            if (vacuumButton != null)
            {
                vacuumButton.onClick.AddListener(() => upgrades.TryUpgradeVacuum());
            }

            if (upgradePanelUI != null)
            {
                upgradePanelUI.gameObject.SetActive(true);
            }
        }
    }
}
