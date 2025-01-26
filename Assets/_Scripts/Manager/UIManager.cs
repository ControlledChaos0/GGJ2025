using TMPro;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private DeathPanel m_deathPanel;
    [SerializeField] private PausePanel m_pausePanel;
    [field: SerializeField] public EnemyTracker EnemyTracker { get; private set; }
    [field: SerializeField] public AmmoDisplay AmmoDisplay { get; private set; }
    [field: SerializeField] public MessageUI Messanger { get; private set; }
    // private BossHealthTracker

    private void Awake()
    {
        InitializeSingleton();
        m_deathPanel.Hide();
        m_pausePanel.Hide();
        EnemyTracker.Hide();
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
    public void OnLevelCombat()
    {
        
    }
    public void OnLevelPuzzle()
    {

    }
    public void OnLevelBoss()
    {

    }
}
