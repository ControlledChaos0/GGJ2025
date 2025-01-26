using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level Specifcations")]
    [SerializeField] private LevelType type;
    [SerializeField] private string nextSceneOverride = "";
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
                UIManager.Instance.Messanger.DisplayeMessage($"Escape to the end");
                break;
            case LevelType.Combat:
                enemyCount = enemies.childCount;
                UIManager.Instance.EnemyTracker.Show();
                UIManager.Instance.Messanger.DisplayeMessage($"Kill {enemyCount} enemies!");
                UIManager.Instance.EnemyTracker.UpdateText(enemyCount.ToString());
                break;
            case LevelType.Boss:
                break;
        }
        
    }
    public void TransitionToNextScene()
    {
        Unpause();
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneOverride.Length > 0) SceneManager.LoadSceneAsync(nextSceneOverride);
        else SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel()
    {
        Unpause();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void BackToMainMenu()
    {
        Unpause();
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
        PlayerController.Instance.Rigidbody.linearVelocity = Vector2.zero;
        PlayerController.Instance.enabled = false;
        StartCoroutine(CombatEndThread());
    }
    private IEnumerator CombatEndThread()
    {
        UIManager.Instance.Messanger.DisplayeMessage($"Defeated all Enemies!", 2f);
        yield return new WaitForSeconds(3f);
        UIManager.Instance.Messanger.DisplayeMessage($"Moving To Next Level!", 10f);
        yield return new WaitForSeconds(2f);
        TransitionToNextScene();
    }
}

public enum LevelType
{
    Combat,
    Puzzle,
    Boss
}
