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
            var app = Instantiate(Resources.Load<App>(_appGameObjectName));

            if (app == null)
            {
                throw new ApplicationException();
            }

            app.name = _appGameObjectName;
            app.InitializeComponents();

            DontDestroyOnLoad(app);
        }

        #endregion

        private void InitializeComponents()
        {
            ProjectContext = _projectContext;
            ProjectContext.Intialize(this);
        }
    }
}