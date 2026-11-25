using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Gameplay
{
    public class GameplayPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private GameObject tapHint;
        [SerializeField] private Image perfectImage;
        [SerializeField] private Button pauseButton;

        private void OnEnable()
        {
            EventManager.OnScoreChanged += HandleScoreChanged;
            EventManager.OnPlayerJumped += HandlePlayerJumped;
            EventManager.OnPlatformLanded += HandlePlatformLanded;
            EventManager.OnGameReset += HandleGameReset;
            pauseButton.onClick.AddListener(HandlePauseClicked);
            tapHint.SetActive(true);
            perfectImage.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EventManager.OnScoreChanged -= HandleScoreChanged;
            EventManager.OnPlayerJumped -= HandlePlayerJumped;
            EventManager.OnPlatformLanded -= HandlePlatformLanded;
            EventManager.OnGameReset -= HandleGameReset;
            pauseButton.onClick.RemoveListener(HandlePauseClicked);
        }

        private void HandleScoreChanged(int currentScore)
        {
            currentScoreText.text = currentScore.ToString();
        }

        private void HandlePlayerJumped()
        {
            tapHint.SetActive(false);
        }

        private void HandlePlatformLanded(PlatformController platform, bool isPerfect)
        {
            if (!isPerfect) return;
            perfectImage.gameObject.SetActive(false);
            perfectImage.gameObject.SetActive(true);
        }

        private void HandleGameReset()
        {
            tapHint.SetActive(true);
            perfectImage.gameObject.SetActive(false);
        }

        private void HandlePauseClicked()
        {
            EventManager.RaiseRequestPause();
        }
    }
}
