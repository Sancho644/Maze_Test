namespace Core.Signals
{
    public class GameStateChangedSignal
    {
        public GameState State { get; }

        public GameStateChangedSignal(GameState state)
        {
            State = state;
        }
    }
}