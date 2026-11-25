using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Home.Shop
{
    public class ShopPanel : Panel
    {
        [SerializeField] private Button characterTabButton;
        [SerializeField] private Button themeTabButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private float selectedTabOffsetY = 30f;

        private float _baseTabY;
        private string _activeTabName;

        private void Awake()
        {
            _baseTabY = characterTabButton.transform.localPosition.y;
        }

        private void OnEnable()
        {
            characterTabButton.onClick.AddListener(OpenCharacterTab);
            themeTabButton.onClick.AddListener(OpenThemeTab);
            closeButton.onClick.AddListener(Close);
        }

        public override void Open()
        {
            base.Open();
            OpenCharacterTab();
        }

        private void OnDisable()
        {
            characterTabButton.onClick.RemoveListener(OpenCharacterTab);
            themeTabButton.onClick.RemoveListener(OpenThemeTab);
            closeButton.onClick.RemoveListener(Close);
            PanelManager.Instance.ClosePanel(GameConfig.CharacterShopPanelName);
            PanelManager.Instance.ClosePanel(GameConfig.ThemeShopPanelName);
            _activeTabName = null;
        }

        private void OpenCharacterTab() => SwitchTab(GameConfig.CharacterShopPanelName);
        private void OpenThemeTab() => SwitchTab(GameConfig.ThemeShopPanelName);

        private void SwitchTab(string panelName)
        {
            if (_activeTabName == panelName) return;
            if (_activeTabName != null) PanelManager.Instance.ClosePanel(_activeTabName);
            _activeTabName = panelName;
            PanelManager.Instance.OpenPanel(panelName);
            UpdateTabVisual(panelName);
        }

        private void UpdateTabVisual(string activePanelName)
        {
            bool isCharacter = activePanelName == GameConfig.CharacterShopPanelName;
            SetTabY(characterTabButton, isCharacter ? _baseTabY + selectedTabOffsetY : _baseTabY);
            SetTabY(themeTabButton, isCharacter ? _baseTabY : _baseTabY + selectedTabOffsetY);
        }

        private static void SetTabY(Button button, float y)
        {
            Vector3 pos = button.transform.localPosition;
            pos.y = y;
            button.transform.localPosition = pos;
        }
    }
}
