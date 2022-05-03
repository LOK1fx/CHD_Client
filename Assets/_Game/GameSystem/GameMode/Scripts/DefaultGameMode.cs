using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class DefaultGameMode : BaseGameMode
{
    protected EGameModeState state;

    public override IEnumerator OnStart()
    {
        Debug.Log("GameMode started");

        var camera = GameObject.Instantiate(Resources.Load("PlayerCamera")) as GameObject;

        RegisterGameModeObject(camera);

        var ui = GameObject.Instantiate(UiPrefab);

        RegisterGameModeObject(ui);

        state = EGameModeState.Started;

        yield return null;
    }

    public override IEnumerator OnEnd()
    {
        state = EGameModeState.Ending;

        yield return DestroyAllGameModeObjects();

        Debug.Log("GameMode endend");

        state = EGameModeState.Ended;
    }
}

public enum EGameModeState
{
    Started,
    Starting,
    Ending,
    Ended
}