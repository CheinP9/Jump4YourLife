using Jump4YourLife.Scripts.Development.Managers;

namespace Jump4YourLife.Scripts.Development.StateMachine.States
{
    public sealed class LoadedState : BaseGameState
    {
        public LoadedState(GameContext context, GameStateMachine stateMachine) : base(context, stateMachine)
        {
        }
        public override void Enter()
        {
            Context.Player.SetGameplayActive(false);
            Context.PlatformPool.SetGameplayActive(false);
            PrepareNewLevel();
            EventManager.OnRequestPlay += HandleRequestPlay;
        }
        public override void Exit()
        {
            EventManager.OnRequestPlay -= HandleRequestPlay;
        }
        private void HandleRequestPlay()
        {
            StateMachine.Fire(GameTrigger.Play);
        }
    }
}
