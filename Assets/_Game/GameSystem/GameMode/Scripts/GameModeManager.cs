using System.Collections;
using UnityEngine;
using LOK1game.Tools;

public class GameModeManager
{
    private IGameMode _currentGameMode;
    private bool _isSwithing;

    public void SwitchGameMode(IGameMode gameMode)
    {
        Coroutines.StartRoutine(SwitchMode(gameMode));
    }

    private IEnumerator SwitchMode(IGameMode gameMode)
    {
        yield return new WaitUntil(() => !_isSwithing);

        if(_currentGameMode == gameMode)
        {
            yield break;
        }

        _isSwithing = true;

        if(_currentGameMode != null)
        {
            yield return _currentGameMode.OnEnd();
        }

        _currentGameMode = gameMode;

        yield return _currentGameMode.OnStart();

        _isSwithing = false;
    }
}