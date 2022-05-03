using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync(1);
    }
}