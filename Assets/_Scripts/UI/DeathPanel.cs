using UnityEngine;

public class DeathPanel : Panel
{
    public override void Show()
    {
        // (Ryan) Can use an alpha/animation reveal in future
        gameObject.SetActive(true);
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnRestart()
    {
        LevelManager.Instance.RestartLevel();
        Hide();
    }
    public void OnBackToMenu()
    {
        LevelManager.Instance.BackToMainMenu();
    }
}
