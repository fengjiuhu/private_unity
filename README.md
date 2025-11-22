# Coin Push Idle (Unity Template)

A lightweight Unity project scaffold for a relaxing coin-pushing idle game. The template provides scripts, prefabs via editor utilities, save/load helpers, and a placeholder scene to quickly iterate on the experience shown in the reference mock.

## Project Structure
```
Assets/
  Scripts/             # Core gameplay, UI, FX, tools, pooling, upgrades, save/load
  Prefabs/             # Auto-generated via CoinPush/Generate Prefabs menu
  Scenes/              # MainScene placeholder
  UI/                  # UI assets (placeholders)
  Resources/           # Materials or runtime-loaded assets
  Materials/
  Models/
  Audio/
  Editor/              # AutoPrefabGenerator and build helpers
```

## Setup
1. Open the folder in **Unity 2022.3+**.
2. Install/enable the **Input System** package and set it as active input handling.
3. Run `CoinPush/Generate Prefabs` from the Unity menu to create placeholder coin, player, and gate prefabs.
4. Open `Assets/Scenes/MainScene.unity` and place the prefabs in the scene with a plane floor and camera.
5. Add a `GameManager` object and assign: `CurrencyManager`, `UIManager`, `UpgradeSystem`, `UpgradeManager`, `PlayerController`, `PlayerUpgrader`, `CoinSpawner`, and `ObjectPooler` (pool id `coins`).
6. Optional: add `SoundManager`, `HapticManager`, `DebugPanel`, and UI widgets (`CurrencyUI`, `LevelPanelUI`, `UpgradePanelUI`, `JoystickUI`, `SettingsUI`, `RewardUI`).
7. Localization: place a `LocalizationManager` in scene (or allow GameManager to auto-create). Add `LocalizedText` to labels and use keys like `currency_gold`, `upgrade_speed`, `button_upgrade` to auto-switch 中/EN 文案。`SettingsUI` includes a dropdown to 切换语言，默认中文。

## Controls
- **Virtual Joystick (Mobile):** Use `MobileJoystick.Direction` to drive `PlayerController.OnMove` through the Input System or `JoystickUI` wrapper.
- **Keyboard/Editor:** WASD/Arrow keys when using an Input Action asset mapped to the `OnMove` callback.

## Core Systems
- **Core:** `GameManager`, `CurrencyManager` (gold/energy/gems with save/load), `UpgradeSystem` + `UpgradeManager`, `SaveSystem`, `EventBus`, `ObjectPooler`, `IdleIncomeSystem` (在线/离线收益)。
- **Player:** `PlayerController`, `PlayerMotor`, `PlayerVacuum`, `PlayerCollisionHandler`, `PlayerUpgrader`, `PlayerStats`.
- **Coins:** `Coin`, `CoinCollector`, `CoinSpawner` (density + pooling), `CoinPhysicsOptimizer`, `CoinDespawnArea`.
- **Gates:** `MultiplierGate`, `GateTrigger`, `GateVisuals`, `MultiplierFX`.
- **UI:** `UIManager`, `CurrencyUI`, `LevelPanelUI`, `UpgradePanelUI`, `JoystickUI`, `SettingsUI`（含语言切换）, `RewardUI`, `ScreenSafeArea`, `LocalizedText`, `LocalizationManager`。
- **Effects/Tools:** `CoinPickupFX`, `UINumberPopup`, `ShakeEffect`, `FloatingTextController`, `SimpleAnimator`, `SoundManager`, `HapticManager`, `DebugPanel`, `BuildToolsEditor`, `LevelEditorWindow`.

## Building
### Android
1. Open **Build Settings** → Android → Switch Platform.
2. Set minimum API level (e.g., Android 8.0) and configure keystore if needed.
3. Use **CoinPush/Build/Android** for a quick development build or press **Build and Run**.

### iOS
1. Switch Platform to **iOS** in Build Settings.
2. Configure bundle identifier and signing in Player Settings.
3. Use **CoinPush/Build/iOS** for a quick development build or export manually to Xcode.

## Reference Image
Place `5c363ef3-f19c-4402-8429-1659a7677aef.jpg` under `Assets/UI/Reference/` if you want the mock available in the editor.
