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
        switch (type)
        {
            case LevelType.Puzzle:
                break;
            case LevelType.Combat:
                enemyCount = enemies.childCount;
                UIManager.Instance.EnemyTracker.Show();
                UIManager.Instance.EnemyTracker.UpdateText(enemyCount.ToString());
                break;
            case LevelType.Boss:
                break;
        }
        
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
        if (type == LevelType.Puzzle) return;
        enemyCount--;
        UIManager.Instance.EnemyTracker.UpdateText(enemyCount.ToString());
        if (enemyCount <= 0)
        {
            UIManager.Instance.EnemyTracker.UpdateText("0");
            CombatLevelEndBehaviour();
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
    private void CombatLevelEndBehaviour()
    {

    }
}

public enum LevelType
{
    Combat,
    Puzzle,
    Boss
}
