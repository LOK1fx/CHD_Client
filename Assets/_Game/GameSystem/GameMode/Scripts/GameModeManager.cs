using System.Collections;
using UnityEngine;
using LOK1game.Tools;

[System.Serializable]
public class GameModeManager
{
    public CrystalCaptureGameMode CrystalCaptureGameMode => _gameMode;

    [SerializeField] private CrystalCaptureGameMode _gameMode;

    public IGameMode CurrentGameMode { get; private set; }

    private bool _isSwithing;

    public void SwitchGameMode(IGameMode gameMode)
    {
        Coroutines.StartRoutine(SwitchMode(gameMode));
    }

    private IEnumerator SwitchMode(IGameMode gameMode)
    {
        yield return new WaitUntil(() => !_isSwithing);

        if(CurrentGameMode == gameMode)
        {
            yield break;
        }

        _isSwithing = true;

        if(CurrentGameMode != null)
        {
            yield return CurrentGameMode.OnEnd();
        }

        CurrentGameMode = gameMode;

        yield return CurrentGameMode.OnStart();

        _isSwithing = false;
    }
}