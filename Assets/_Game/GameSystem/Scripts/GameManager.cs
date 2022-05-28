using LOK1game.Game.Events;

namespace LOK1game.Game
{
    public enum EGameState
    {
        Going,
        Paused
    }

    public class GameManager : PersistentSingleton<GameManager>
    {
        public EGameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(EGameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        public void SetState(EGameState gameState)
        {
            if (gameState == CurrentGameState)
                return;

            var evt = Events.Events.OnGameStateChanged;
            evt.PreviousState = CurrentGameState;
            evt.NewGameState = gameState;

            CurrentGameState = gameState;

            OnGameStateChanged?.Invoke(CurrentGameState);
            EventManager.Broadcast(evt);
        }
    }
}