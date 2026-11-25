using Jump4YourLife.Scripts.Development.Controllers.Platform;
using Jump4YourLife.Scripts.Development.Managers;

namespace Jump4YourLife.Scripts.Development.StateMachine
{
    public abstract class BaseGameState : State
    {
        protected GameContext Context { get; }
        protected GameStateMachine StateMachine { get; }
        protected BaseGameState(GameContext context, GameStateMachine stateMachine)
        {
            Context = context;
            StateMachine = stateMachine;
        }
        protected void PrepareNewLevel()
        {
            Context.ResetScore();
            EventManager.RaiseGameReset();
            PlatformController firstPlatform = Context.PlatformSpawner.BuildInitialLevel();
            Context.Player.SpawnOn(firstPlatform);
        }
    }
}
