using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Home
{
    public class HomePanel : Panel
    {
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(HandlePlayClicked);
            shopButton.onClick.AddListener(HandleShopClicked);
            settingButton.onClick.AddListener(HandleSettingClicked);
            RefreshView();
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(HandlePlayClicked);
            shopButton.onClick.RemoveListener(HandleShopClicked);
            settingButton.onClick.RemoveListener(HandleSettingClicked);
        }

        public void RefreshView()
        {
            PlayerSaveData saveData = SaveManager.LoadPlayerData();
            highScoreText.text = $"{saveData.highScore}";
        }

        private void HandlePlayClicked() => SceneManager.LoadScene(GameConfig.GameplaySceneName);
        private void HandleShopClicked() => PanelManager.Instance.OpenPanel(GameConfig.ShopPanelName);
        private void HandleSettingClicked() => PanelManager.Instance.OpenPanel(GameConfig.SettingsModalName);
    }
}
