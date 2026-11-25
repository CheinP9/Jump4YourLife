using Cysharp.Threading.Tasks;
using Jump4YourLife.Scripts.Core.Character;
using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Core.Theme;
using Jump4YourLife.Scripts.Development.Controllers.CameraSystem;
using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Controllers.Player;
using Jump4YourLife.Scripts.Development.Controllers.Theme;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using Jump4YourLife.Scripts.Development.Pools;
using Jump4YourLife.Scripts.Development.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jump4YourLife.Scripts.Development
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterDatabase characterDatabase;
        [SerializeField] private ThemeDatabase themeDatabase;

        [SerializeField] private BackgroundController gameplayBackground;
        [SerializeField] private ScreenWalls screenWalls;
        [SerializeField] private PlatformPool platformPool;
        [SerializeField] private PlatformSpawner platformSpawner;
        [SerializeField] private PlayerController player;

        private GameStateController _gameStateController;
        private PlayerSaveData _playerSaveData;

        private async void Start()
        {
            _playerSaveData = SaveManager.LoadPlayerData();

            CharacterData selectedCharacter = characterDatabase.GetCharacterById(_playerSaveData.selectedCharacterId);
            ThemeData selectedTheme = themeDatabase.GetThemeById(_playerSaveData.selectedThemeId);

            gameplayBackground.SetTheme(_playerSaveData.selectedThemeId);
            screenWalls.SetWallSprite(selectedTheme.WallSprite);
            platformPool.Initialize();
            platformSpawner.SetTheme(selectedTheme);
            player.Initialize(selectedCharacter);

            GameContext context = new GameContext(platformSpawner, platformPool, player, _playerSaveData);
            _gameStateController = new GameStateController(context);
            EventManager.OnRequestHome += HandleRequestHome;
            await PanelManager.Instance.OpenPanelAsync(GameConfig.GameplayPanelName);
            _gameStateController.Initialize();
        }

        private void HandleRequestHome()
        {
            SceneManager.LoadScene(GameConfig.HomeSceneName);
        }

        private void OnDestroy()
        {
            EventManager.OnRequestHome -= HandleRequestHome;
            SaveManager.SavePlayerData(_playerSaveData);
            _gameStateController?.Dispose();
        }
    }
}
