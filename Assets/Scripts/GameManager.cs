using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Central bootstrapper for the coin pushing idle game.
    /// Handles scene initialization and provides access to shared managers.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private UpgradeSystem upgradeSystem;
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private ObjectPooler objectPooler;
        [SerializeField] private CoinSpawner coinSpawner;
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerUpgrader playerUpgrader;
        [SerializeField] private LocalizationManager localizationManager;
        [SerializeField] private IdleIncomeSystem idleIncomeSystem;

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }

                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeManagers();
        }

        private void InitializeManagers()
        {
            currencyManager ??= FindObjectOfType<CurrencyManager>();
            uiManager ??= FindObjectOfType<UIManager>();
            upgradeSystem ??= FindObjectOfType<UpgradeSystem>();
            upgradeManager ??= FindObjectOfType<UpgradeManager>();
            objectPooler ??= FindObjectOfType<ObjectPooler>();
            coinSpawner ??= FindObjectOfType<CoinSpawner>();
            player ??= FindObjectOfType<PlayerController>();
            playerUpgrader ??= FindObjectOfType<PlayerUpgrader>();
            localizationManager ??= FindObjectOfType<LocalizationManager>();
            idleIncomeSystem ??= FindObjectOfType<IdleIncomeSystem>();

            if (localizationManager == null)
            {
                localizationManager = new GameObject("LocalizationManager").AddComponent<LocalizationManager>();
            }
            currencyManager?.Initialize();
            playerUpgrader?.Initialize(currencyManager);
            upgradeSystem?.Initialize(currencyManager, player);
            upgradeManager?.Initialize(playerUpgrader != null ? playerUpgrader.GetComponent<PlayerMotor>()?.GetStats() : null, currencyManager);
            uiManager?.Initialize(currencyManager, upgradeSystem, player);
            coinSpawner?.BeginSpawning();
            idleIncomeSystem?.Initialize(currencyManager);
        }

        private void OnApplicationQuit()
        {
            EventBus.SaveRequested();
        }
    }
}
