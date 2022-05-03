using UnityEngine;
using System;

public class App : MonoBehaviour
{
    public GameModeManager GameModeManager { get; private set; }

    [SerializeField] private DefaultGameMode _gameMode;

    private const string _appGameobjectName = "[App]";

    #region Boot

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Boot()
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
        GameModeManager = new GameModeManager();
    }

    private void Start()
    {
        GameModeManager.SwitchGameMode(_gameMode);
    }
}