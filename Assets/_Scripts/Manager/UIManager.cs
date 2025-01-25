using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private DeathPanel m_deathPanel;
    [SerializeField] private GameObject m_pausePanel;

    private void Awake()
    {
        InitializeSingleton();
        m_deathPanel.Hide();
        m_pausePanel.SetActive(false);
    }
    public void ShowPausePanel()
    {
        m_pausePanel.SetActive(true);
        BubbleBlowerCursor.Instance.gameObject.SetActive(false);
    }
    public void HidePausePanel() 
    {
        m_pausePanel.SetActive(false);
        BubbleBlowerCursor.Instance.gameObject.SetActive(true);
    }

    public void ShowDeathPanel()
    {
        m_deathPanel.Show();
        BubbleBlowerCursor.Instance.gameObject.SetActive(false);
    }
}
