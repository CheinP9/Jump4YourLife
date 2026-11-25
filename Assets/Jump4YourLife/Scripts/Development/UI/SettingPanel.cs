using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI
{
    public class SettingPanel : Panel
    {
        [SerializeField] private Image titleImage;
        [SerializeField] private Sprite settingTitleSprite;
        [SerializeField] private Sprite pauseTitleSprite;
        [SerializeField] private Button soundToggleButton;
        [SerializeField] private Image soundIconImage;
        [SerializeField] private Sprite allowSoundSprite;
        [SerializeField] private Sprite muteSoundSprite;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button closeButton;
        private bool _isInGame;

        private void OnEnable()
        {
            _isInGame = SceneManager.GetActiveScene().name == GameConfig.GameplaySceneName;
            titleImage.sprite = _isInGame ? pauseTitleSprite : settingTitleSprite;
            homeButton.gameObject.SetActive(_isInGame);
            restartButton.gameObject.SetActive(_isInGame);
            creditButton.gameObject.SetActive(!_isInGame);
            UpdateSoundIcon();
            soundToggleButton.onClick.AddListener(HandleSoundToggleClicked);
            restartButton.onClick.AddListener(HandleRestartClicked);
            creditButton.onClick.AddListener(HandleCreditClicked);
            closeButton.onClick.AddListener(HandleCloseClicked);
            homeButton.onClick.AddListener(HandleHomeClicked);
        }

        private void OnDisable()
        {
            soundToggleButton.onClick.RemoveListener(HandleSoundToggleClicked);
            restartButton.onClick.RemoveListener(HandleRestartClicked);
            creditButton.onClick.RemoveListener(HandleCreditClicked);
            closeButton.onClick.RemoveListener(HandleCloseClicked);
            homeButton.onClick.RemoveListener(HandleHomeClicked);
        }

        private void HandleSoundToggleClicked()
        {
            AudioManager.Instance?.ToggleMute();
            UpdateSoundIcon();
        }

        private void UpdateSoundIcon()
        {
            bool isMuted = AudioManager.Instance.IsMuted;
            soundIconImage.sprite = isMuted ? muteSoundSprite : allowSoundSprite;
        }

        private void HandleRestartClicked()
        {
            EventManager.RaiseRequestRestart();
        }

        private void HandleCloseClicked()
        {
            if (_isInGame)
            {
                EventManager.RaiseRequestPlay();
            }
            else
            {
                Close();
            }
        }

        private void HandleHomeClicked()
        {
            EventManager.RaiseRequestHome();
        }

        private void HandleCreditClicked()
        {
            PanelManager.Instance.OpenPanel(GameConfig.CreditPanelName);
        }
    }
}
