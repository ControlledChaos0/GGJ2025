using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level Specifcations")]
    [SerializeField] private LevelType type;
    [SerializeField] private string nextScene;
    [SerializeField] private Transform enemies;
    private int enemyCount;
    private bool paused;
    private void Awake()
    {
        InitializeSingleton();
    }
    private void Start()
    {
        enemyCount = enemies.childCount;
    }
    public void UnlockExitDoor()
    {
        // Exit Door Behaviour
    }
    public void TransitionToNextScene()
    {
        SceneManager.LoadSceneAsync(nextScene);
    }
    public void RestartLevel()
    {
        Unpause();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("title");
    }
    public void OnEnemyDeath()
    {
        if (--enemyCount <= 0)
        {
            UnlockExitDoor();
        }
    }
    public void OnPlayerDeath()
    {
        UIManager.Instance.ShowDeathPanel();
    }
    public void Pause()
    {
        if (paused) return;
        paused = true;
        UIManager.Instance.ShowPausePanel();
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        if (!paused) return;
        paused = false;
        UIManager.Instance.HidePausePanel();
        Time.timeScale = 1f;
    }
    public void TogglePause()
    {
        if (paused) Unpause();
        else Pause();
    }
}

public enum LevelType
{
    Combat,
    Puzzle,
    Boss
}
