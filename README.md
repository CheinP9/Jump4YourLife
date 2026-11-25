# Jump 4 Your Life

A 2D vertical arcade jumping game built with **Unity 6 (`6000.3.19f1`)**.

## Features
- **One-Tap Arcade Gameplay**: Jump down from moving platforms, chain **Perfect** landings for combo points, and avoid falling or staying on a platform until it breaks on wall hits.
- **Character & Theme Shop**: Unlock 9 playable characters based on your high score and switch between 5 visual themes (background, walls, and platforms).
- **Audio & Settings**: Background music, SFX, mute persistence, pause/resume, and credits modal.
- **Persistent Save System**: JSON-serialized `PlayerSaveData` stored via `PlayerPrefs`.

## Architecture
- **State Machine**: `LoadedState` → `StartedState` ↔ `PausedState` / `LostState` orchestrated via `GameStateController` and `GameContext`.
- **Event-Driven Decoupling**: `EventManager` decouples input, gameplay controllers, UI panels, and audio triggers.
- **Object Pooling**: `PlatformPool` reuses platforms for smooth, allocation-free infinite scrolling.
- **Async UI Management**: `PanelManager` lazy-loads UI panels on demand with **Addressables** and **UniTask**.

## Project Structure
```text
Assets/Jump4YourLife/
├── Animations/      # Character & UI animation clips / controllers
├── Audio/           # BGM and SFX clips
├── Data/            # ScriptableObject databases (Characters, Themes)
├── Materials/       # 2D physics materials
├── Prefabs/         # Character skins and Platform prefab
├── Scenes/          # HomeScene & GameplayScene
├── Scripts/
│   ├── Core/        # Config, Enums, ScriptableObject data models
│   └── Development/ # Controllers, Managers, Pools, LoadSave, StateMachine, UI
└── UI/              # Addressable UI panel prefabs
```

## Getting Started
1. Clone this repository.
2. Open the project folder in **Unity Hub** using **Unity `6000.3.19f1`** (or newer Unity 6).
3. Open `Assets/Jump4YourLife/Scenes/HomeScene.unity` and press **Play**.
