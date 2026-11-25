using Jump4YourLife.Scripts.Core.Config;
using Jump4YourLife.Scripts.Core.Enums;
using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Managers;

namespace Jump4YourLife.Scripts.Development.StateMachine.States
{
    public sealed class StartedState : BaseGameState
    {
        public StartedState(GameContext context, GameStateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public override void Enter()
        {
            Context.Player.SetGameplayActive(true);
            Context.PlatformPool.SetGameplayActive(true);
            EventManager.OnPlatformLanded += HandlePlatformLanded;
            EventManager.OnPlayerJumped += HandlePlayerJumped;
            EventManager.OnPlatformCracked += HandlePlatformCracked;
            EventManager.OnPlatformBroken += HandlePlatformBroken;
            EventManager.OnLoseLevel += HandleLoseLevel;
            EventManager.OnRequestPause += HandleRequestPause;
            EventManager.RaiseScoreChanged(Context.CurrentScore);
        }

        public override void Exit()
        {
            EventManager.OnPlatformLanded -= HandlePlatformLanded;
            EventManager.OnPlayerJumped -= HandlePlayerJumped;
            EventManager.OnPlatformCracked -= HandlePlatformCracked;
            EventManager.OnPlatformBroken -= HandlePlatformBroken;
            EventManager.OnLoseLevel -= HandleLoseLevel;
            EventManager.OnRequestPause -= HandleRequestPause;
        }

        private void HandlePlatformLanded(PlatformController platform, bool isPerfect)
        {
            if (isPerfect)
            {
                Context.Combo++;
                Context.CurrentScore += GameConfig.BaseScore + Context.Combo;
                AudioManager.PlaySound(AudioLibrarySounds.Perfect);
            }
            else
            {
                Context.Combo = 0;
                Context.CurrentScore += GameConfig.BaseScore;
            }
            if (Context.CurrentScore > Context.PlayerSaveData.highScore)
            {
                Context.PlayerSaveData.highScore = Context.CurrentScore;
            }
            EventManager.RaiseScoreChanged(Context.CurrentScore);
        }

        private void HandlePlayerJumped() => AudioManager.PlaySound(AudioLibrarySounds.Jump);
        private void HandlePlatformCracked() => AudioManager.PlaySound(AudioLibrarySounds.Crack);
        private void HandlePlatformBroken() => AudioManager.PlaySound(AudioLibrarySounds.Break);

        private void HandleRequestPause()
        {
            StateMachine.Fire(GameTrigger.Pause);
        }

        private void HandleLoseLevel()
        {
            StateMachine.Fire(GameTrigger.Lose);
        }
    }
}
