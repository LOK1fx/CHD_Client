using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class DefaultGameMode : BaseGameMode
{
    public override IEnumerator OnStart()
    {
        Debug.Log("GameMode started");

        var camera = GameObject.Instantiate(Resources.Load("PlayerCamera")) as GameObject;

        RegisterGameModeObject(camera);

        var ui = GameObject.Instantiate(UiPrefab);

        RegisterGameModeObject(ui);

        State = EGameModeState.Started;

        yield return null;
    }

    public override IEnumerator OnEnd()
    {
        State = EGameModeState.Ending;

        yield return DestroyAllGameModeObjects();

        Debug.Log("GameMode endend");

        State = EGameModeState.Ended;
    }
}