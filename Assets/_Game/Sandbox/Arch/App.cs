using LOK1game.Weapon;
using UnityEngine;
using System;
using LOK1game.Game;

public class App : PersistentSingleton<App>
{
    /*
    public static App Instance
    {
        get => _instance;

        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.LogWarning($"{nameof(App)} instane already exist!");

                Destroy(value);

                Debug.Log($"Duplicate of {nameof(App)} has been destroyed.");
            }
        }
    }

    private static App _instance;
    */

    public GameModeManager GameModeManager => _gameModeManager;
    public LevelManager LevelManager => _levelManager;
    public WeaponManager WeaponManager => _weaponManager;

    [Header("GameModes")]
    [SerializeField] private GameModeManager _gameModeManager;
    [SerializeField] private EGameModeId _standardGameModeId;
    [SerializeField] private DefaultGameMode _defaultGameMode = new DefaultGameMode(EGameModeId.Default);
    [SerializeField] private CrystalCaptureGameMode _crystalCaptureGameMode = new CrystalCaptureGameMode(EGameModeId.CrystalCapture);
    [SerializeField] private PVEDefendGameMode _pveDefendGameMode = new PVEDefendGameMode(EGameModeId.PVE);
    [Space]
    [Space]
    [SerializeField] private LevelManager _levelManager = new LevelManager();
    [SerializeField] private WeaponManager _weaponManager = new WeaponManager();

    private const string _appGameobjectName = "[App]";

    #region Boot

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        var app = Instantiate(Resources.Load(_appGameobjectName)) as GameObject;

        if (app == null)
        {
            throw new ApplicationException();
        }

        app.name = _appGameobjectName;

        DontDestroyOnLoad(app);
    }

    #endregion

    private void Awake()
    {
        //Instance = this;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        LevelManager.Initialize();
        _weaponManager.Initialize();
        _gameModeManager = new GameModeManager();

        _gameModeManager.IntializeGameMode(EGameModeId.Default, _defaultGameMode);
        _gameModeManager.IntializeGameMode(EGameModeId.CrystalCapture, _crystalCaptureGameMode);
        _gameModeManager.IntializeGameMode(EGameModeId.PVE, _pveDefendGameMode);
        _gameModeManager.SetGameMode(_standardGameModeId);
    }
}