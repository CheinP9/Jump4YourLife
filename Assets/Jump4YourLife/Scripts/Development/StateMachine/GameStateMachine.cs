using System;
using System.Collections.Generic;

namespace Jump4YourLife.Scripts.Development.StateMachine
{
    public class GameStateMachine : IDisposable
    {
        private readonly Dictionary<GameState, State> _states = new Dictionary<GameState, State>();
        private readonly Dictionary<GameState, Dictionary<GameTrigger, GameState>> _transitions = new Dictionary<GameState, Dictionary<GameTrigger, GameState>>();
        private State _currentState;
        public GameState CurrentState { get; private set; }
        public void AddState(GameState state, State instance)
        {
            _states.Add(state, instance);
        }
        public void AddTransition(GameState from, GameTrigger trigger, GameState to)
        {
            if (!_transitions.TryGetValue(from, out Dictionary<GameTrigger, GameState> transitions))
            {
                transitions = new Dictionary<GameTrigger, GameState>();
                _transitions.Add(from, transitions);
            }
            transitions.Add(trigger, to);
        }
        public void Initialize(GameState initialState)
        {
            _currentState = _states[initialState];
            CurrentState = initialState;
            _currentState.Enter();
        }
        public void Fire(GameTrigger trigger)
        {
            if (_currentState == null) return;
            if (!_transitions.TryGetValue(CurrentState, out Dictionary<GameTrigger, GameState> transitions) ||
                !transitions.TryGetValue(trigger, out GameState nextState))
            {
                return;
            }

            State nextStateInstance = _states[nextState];
            _currentState.Exit();
            CurrentState = nextState;
            _currentState = nextStateInstance;
            _currentState.Enter();
        }
        public void Stop()
        {
            if (_currentState == null) return;
            _currentState.Exit();
            _currentState = null;
        }
        public void Dispose()
        {
            Stop();
        }
    }
}
