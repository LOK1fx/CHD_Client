using UnityEngine;

namespace LOK1game.World
{
    public abstract class GameWorld : MonoBehaviour
    {
        [SerializeField] private EGameModeId _standardGameModeOverride;

        protected void Awake()
        {
            App.ProjectContext.OnInitialized += OnProjectContextInitalized;
        }

        protected void OnProjectContextInitalized()
        {
            var gameModeManager = App.ProjectContext.GameModeManager;

            if (gameModeManager.CurrentGameModeId == _standardGameModeOverride
                && gameModeManager.CurrentGameModeId != EGameModeId.None || _standardGameModeOverride == EGameModeId.None)
            {
                return;
            }

            gameModeManager.SetGameMode(_standardGameModeOverride);
        }

        public abstract void Intialize();
    }
}