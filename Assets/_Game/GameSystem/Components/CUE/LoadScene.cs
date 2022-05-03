using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.LOK1game.MaxterGamejam
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(string name)
        {
            SceneManager.LoadSceneAsync(name);
        }
    }
}