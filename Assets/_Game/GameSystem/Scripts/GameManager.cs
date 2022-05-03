using UnityEngine;

namespace LOK1game.Game
{
    public class GameManager : MonoBehaviour
    {
        public GameManager Instance { get; set; }

        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}