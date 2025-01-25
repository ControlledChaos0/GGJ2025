using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private DeathPanel m_deathPanel;
    [SerializeField] private PausePanel m_pausePanel;

    private void Awake()
    {
        InitializeSingleton();
        m_deathPanel.Hide();
        m_pausePanel.Hide();
    }
    public void ShowPausePanel()
    {
        m_pausePanel.Show();
        BubbleBlowerCursor.Instance.gameObject.SetActive(false);
    }
    public void HidePausePanel() 
    {
        m_pausePanel.Hide();
        BubbleBlowerCursor.Instance.gameObject.SetActive(true);
    }

    public void ShowDeathPanel()
    {
        m_deathPanel.Show();
        BubbleBlowerCursor.Instance.gameObject.SetActive(false);
    }
}
