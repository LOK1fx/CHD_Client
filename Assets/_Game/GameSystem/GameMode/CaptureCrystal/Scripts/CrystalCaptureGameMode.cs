using System;
using System.Collections;
using UnityEngine;

namespace LOK1game.Game
{
    [Serializable]
    public class CrystalCaptureGameMode : BaseGameMode
    {
        #region Events

        public event Action<int> OnProgressChanged;

        #endregion

        public int ProgressGoal { get; private set; }
        public int CurrentProgress { get; private set; }

        public void AddProgress(int value)
        {
            CurrentProgress += value;

            OnProgressChanged?.Invoke(value);
        }

        public override IEnumerator OnStart()
        {
            State = EGameModeState.Starting;

            var camera = Instantiate(Data.CameraPrefab);
            var ui = Instantiate(Data.UiPrefab);


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