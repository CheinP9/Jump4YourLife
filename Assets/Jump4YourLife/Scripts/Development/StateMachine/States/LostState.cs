using Cysharp.Threading.Tasks;
using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Core.Enums;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using Jump4YourLife.Scripts.Development.UI;
using Jump4YourLife.Scripts.Development.UI.Gameplay;

namespace Jump4YourLife.Scripts.Development.StateMachine.States
{
    public sealed class LostState : BaseGameState
    {
        public LostState(GameContext context, GameStateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public override void Enter()
        {
            Context.Player.SetGameplayActive(false);
            Context.PlatformPool.SetGameplayActive(false);
            SaveManager.SavePlayerData(Context.PlayerSaveData);

            AudioManager.PlaySound(AudioLibrarySounds.GameOver);
            OpenGameOverPanel().Forget();
            EventManager.OnRequestPlay += HandleRequestPlay;
        }

        private async UniTaskVoid OpenGameOverPanel()
        {
            Panel panel = await PanelManager.Instance.GetPanelAsync(GameConfig.GameOverPanelName);
            if (StateMachine.CurrentState != GameState.Lost) return;
            if (panel is GameOverPanel gameOverPanel)
            {
                gameOverPanel.SetScore(Context.CurrentScore);
            }
            panel.Open();
        }

        private void HandleRequestPlay()
        {
            PrepareNewLevel();
            StateMachine.Fire(GameTrigger.Play);
        }

        public override void Exit()
        {
            EventManager.OnRequestPlay -= HandleRequestPlay;
            PanelManager.Instance?.ClosePanel(GameConfig.GameOverPanelName);
        }
    }
}
