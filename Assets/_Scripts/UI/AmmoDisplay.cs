using TMPro;
using UnityEngine;

public class AmmoDisplay : Panel
{
    [SerializeField] private GameObject[] m_shells;
    [SerializeField] private TextMeshProUGUI m_text;
    private void Start()
    {
        m_text.SetText("");
    }

    public void ChangeAmmo(int ammo)
    {
        int m = ammo;
        foreach (GameObject go in m_shells)
        {
            go.SetActive(m-- > 0);
        }
        m_text.SetText(ammo == 0 ? "Reloading..." : "");
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
}
