using System;
using UnityEngine;

namespace LOK1game
{
    public class PlayerHudMobileInputManager : MonoBehaviour
    {
        public static event Action OnShootButtonClicked;

        public void ShootClicked()
        {
            OnShootButtonClicked?.Invoke();
        }
    }
}