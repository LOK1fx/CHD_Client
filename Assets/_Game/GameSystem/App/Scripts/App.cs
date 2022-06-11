using UnityEngine;
using System;

namespace LOK1game
{
    public sealed class App : MonoBehaviour
    {
        public static ProjectContext ProjectContext { get; private set; }

        [SerializeField] private ProjectContext _projectContext = new ProjectContext();

        private const string _appGameObjectName = "[App]";

        #region Boot

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            var app = Instantiate(Resources.Load(_appGameObjectName)) as GameObject;

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = _appGameObjectName;

            DontDestroyOnLoad(app);
        }

        #endregion

        private void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ProjectContext = _projectContext;

            ProjectContext.Intialize(this);
        }
    }
}