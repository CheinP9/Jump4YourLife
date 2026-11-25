using Cysharp.Threading.Tasks;
using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Development.LoadSave;
using Jump4YourLife.Scripts.Development.Managers;
using Jump4YourLife.Scripts.Development.UI;

namespace Jump4YourLife.Scripts.Development.StateMachine.States
{
    public sealed class PausedState : BaseGameState
    {
        public PausedState(GameContext context, GameStateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public override void Enter()
        {
            Context.Player.SetGameplayActive(false);
            Context.PlatformPool.SetGameplayActive(false);
            SaveManager.SavePlayerData(Context.PlayerSaveData);
            OpenSettings().Forget();
            EventManager.OnRequestPlay += HandleRequestPlay;
            EventManager.OnRequestRestart += HandleRequestRestart;
        }

        private async UniTaskVoid OpenSettings()
        {
            Panel panel = await PanelManager.Instance.GetPanelAsync(GameConfig.SettingsModalName);
            if (StateMachine.CurrentState != GameState.Paused) return;
            panel.Open();
        }

        public override void Exit()
        {
            EventManager.OnRequestPlay -= HandleRequestPlay;
            EventManager.OnRequestRestart -= HandleRequestRestart;
            PanelManager.Instance?.ClosePanel(GameConfig.SettingsModalName);
        }

        private void HandleRequestPlay()
        {
            StateMachine.Fire(GameTrigger.Play);
        }

        private void HandleRequestRestart()
        {
            PrepareNewLevel();
            StateMachine.Fire(GameTrigger.Play);
        }
    }
}
