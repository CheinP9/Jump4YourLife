using System;
using Jump4YourLife.Scripts.Development.StateMachine.States;

namespace Jump4YourLife.Scripts.Development.StateMachine
{
    public class GameStateController : IDisposable
    {
        public GameStateMachine StateMachine { get; }

        public GameStateController(GameContext context)
        {
            StateMachine = new GameStateMachine();
            StateMachine.AddState(GameState.Loaded, new LoadedState(context, StateMachine));
            StateMachine.AddState(GameState.Started, new StartedState(context, StateMachine));
            StateMachine.AddState(GameState.Paused, new PausedState(context, StateMachine));
            StateMachine.AddState(GameState.Lost, new LostState(context, StateMachine));
            ConfigureTransitions();
        }
        private void ConfigureTransitions()
        {
            StateMachine.AddTransition(GameState.Loaded, GameTrigger.Play, GameState.Started);
            StateMachine.AddTransition(GameState.Started, GameTrigger.Pause, GameState.Paused);
            StateMachine.AddTransition(GameState.Started, GameTrigger.Lose, GameState.Lost);
            StateMachine.AddTransition(GameState.Paused, GameTrigger.Play, GameState.Started);
            StateMachine.AddTransition(GameState.Lost, GameTrigger.Play, GameState.Started);
        }
        public void Initialize()
        {
            StateMachine.Initialize(GameState.Loaded);
            StateMachine.Fire(GameTrigger.Play);
        }
        public void Dispose()
        {
            StateMachine.Dispose();
        }
    }
}
