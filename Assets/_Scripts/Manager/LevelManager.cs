using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level Specifcations")]
    [SerializeField] private LevelType type;
    [SerializeField] private string nextScene;
    [SerializeField] private Transform enemies;
    private int enemyCount;
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
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
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
}

public enum LevelType
{
    Combat,
    Puzzle,
    Boss
}
