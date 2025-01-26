using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string firstScene;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(firstScene);
    }
    public void PlayLevel(int level)
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + level);
    }
}
