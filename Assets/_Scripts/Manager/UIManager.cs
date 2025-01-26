using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] private DeathPanel m_deathPanel;
    [SerializeField] private PausePanel m_pausePanel;
    [SerializeField] private TextMeshProUGUI ammoText;

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

    public void ShowAmmo() 
    {
        ammoText.gameObject.SetActive(true);
    }

    public void HideAmmo() 
    {
        ammoText.gameObject.SetActive(false);
    }

    public void ChangeAmmo(int ammo) 
    {
        if (ammo == 0) {
            ammoText.SetText("Reloading");
        } else {
            ammoText.SetText($"Ammo: {ammo}");

        }
    }
}
