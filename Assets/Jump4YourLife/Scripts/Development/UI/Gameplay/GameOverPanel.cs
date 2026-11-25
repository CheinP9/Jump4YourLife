using Jump4YourLife.Scripts.Development.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jump4YourLife.Scripts.Development.UI.Gameplay
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private Button restartButton;
        private void OnEnable()
        {
            restartButton.onClick.AddListener(HandleRestartClicked);
        }
        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(HandleRestartClicked);
        }

        public void SetScore(int finalScore)
        {
            finalScoreText.text = finalScore.ToString();
        }
        private void HandleRestartClicked() => EventManager.RaiseRequestPlay();
    }
}
