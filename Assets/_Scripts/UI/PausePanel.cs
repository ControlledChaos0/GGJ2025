using UnityEngine;

public class PausePanel : Panel
{
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public void OnBackToGame()
    {
        LevelManager.Instance.Unpause();
    }
    public void OnRestart()
    {
        LevelManager.Instance.RestartLevel();
    }
    public void OnBackToMainMenu()
    {
        LevelManager.Instance.BackToMainMenu();
    }
}
