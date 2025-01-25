using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private GameObject m_deathPanel;
    [SerializeField] private GameObject m_pausePanel;

    private void Awake()
    {
        InitializeSingleton();
        m_deathPanel.SetActive(false);
        m_pausePanel.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        // (Ryan) Can use an alpha/animation reveal in future
        m_deathPanel.SetActive(true);
    }
}
