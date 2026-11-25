using Cysharp.Threading.Tasks;
using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.Controllers.Theme;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development
{
    public class HomeEntryPoint : MonoBehaviour
    {
        [SerializeField] private BackgroundController homeBackground;

        private async void Start()
        {
            PlayerSaveData saveData = SaveManager.LoadPlayerData();
            homeBackground.SetTheme(saveData.selectedThemeId);
            EventManager.OnThemeSelected += HandleThemeSelected;
            await PanelManager.Instance.OpenPanelAsync(GameConfig.HomePanelName);
        }
        private void HandleThemeSelected(int themeId)
        {
            homeBackground.SetTheme(themeId);
        }
        private void OnDestroy()
        {
            EventManager.OnThemeSelected -= HandleThemeSelected;
        }
    }
}
