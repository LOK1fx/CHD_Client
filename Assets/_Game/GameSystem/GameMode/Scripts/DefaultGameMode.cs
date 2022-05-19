using System.Collections;
using UnityEngine;
using System;

namespace LOK1game.Game
{
    [Serializable]
    public sealed class DefaultGameMode : BaseGameMode
    {
        public DefaultGameMode(EGameModeId id)
        {
            _id = id;
        }

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            var camera = GameObject.Instantiate(CameraPrefab);
            var ui = GameObject.Instantiate(UiPrefab);


            RegisterGameModeObject(camera);
            RegisterGameModeObject(ui);

            State = EGameModeState.Started;

            yield return null;
        }

        public override IEnumerator OnEnd()
        {
            State = EGameModeState.Ending;

            yield return DestroyAllGameModeObjects();

            State = EGameModeState.Ended;
        }
    }
}