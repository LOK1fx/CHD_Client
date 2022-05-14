using LOK1game.Weapon;
using UnityEngine;
using System;

public class App : MonoBehaviour
{
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

    public GameModeManager GameModeManager => _gameModeManager;
    public LevelManager LevelManager => _levelManager;

    [SerializeField] private GameModeManager _gameModeManager = new GameModeManager();
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
        _instance = this;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        GameModeManager.SwitchGameMode(GameModeManager.CrystalCaptureGameMode);
        LevelManager.Initialize();
        _weaponManager.Initialize();
    }
}