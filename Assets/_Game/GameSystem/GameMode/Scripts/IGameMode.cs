using System.Collections;

namespace LOK1game.Game
{
    public interface IGameMode
    {
        EGameModeId Id { get; }
        IEnumerator OnStart();
        IEnumerator OnEnd();
    }
}