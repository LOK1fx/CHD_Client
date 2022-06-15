using LOK1game.Game;
using LOK1game.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [System.Serializable]
    public sealed class ProjectContext : Context
    {
        public GameModeManager GameModeManager => _gameModeManager;
        public LevelManager LevelManager => _levelManager;
        public WeaponManager WeaponManager => _weaponManager;

        [Header("GameModes")]
        [SerializeField] private GameModeManager _gameModeManager;
        [SerializeField] private EGameModeId _standardGameModeId;
        [SerializeField] private List<BaseGameMode> _gameModes = new List<BaseGameMode>();
        [Space]
        [Space]
        [SerializeField] private LevelManager _levelManager = new LevelManager();
        [SerializeField] private WeaponManager _weaponManager = new WeaponManager();

        public override void Intialize(App app)
        {
            App = app;

            LevelManager.Initialize();
            _weaponManager.Initialize();
            _gameModeManager = new GameModeManager();

            foreach (var gamemode in _gameModes)
            {
                _gameModeManager.AddGameMode(gamemode.Id, gamemode);
            }

            _gameModeManager.SetGameMode(_standardGameModeId);
        }
    }
}
