# Jump 4 Your Life — Architecture

## Project Structure
```text
Assets/Jump4YourLife/
├── Animations/          # Character & UI animation clips / controllers
├── Audio/               # BGM and SFX clips
├── Data/                # ScriptableObject databases (Characters, Themes)
├── Materials/           # 2D physics materials
├── Prefabs/             # Character skins and Platform prefab
├── Scenes/              # HomeScene & GameplayScene
├── Scripts/
│   ├── Core/            # Config, Enums, ScriptableObject data models
│   └── Development/
│       ├── Controllers/ # Player, Platform, CameraSystem, Theme, GameOverTrigger
│       ├── LoadSave/    # PlayerSaveData, SaveManager
│       ├── Managers/    # AudioManager, EventManager, PanelManager
│       ├── Pools/       # PlatformPool
│       ├── StateMachine/# GameStateMachine, GameStateController, GameContext, States
│       ├── UI/          # Panel base, Home, Shop, Gameplay, Setting, Credit
│       └── Utils/       # Singleton
└── UI/                  # Addressable UI panel prefabs
```
