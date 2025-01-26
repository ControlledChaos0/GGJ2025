using TMPro;
using UnityEngine;

public class AmmoDisplay : Panel
{
    [SerializeField] private GameObject[] m_shells;
    [SerializeField] private GameObject rotatingGear;
    private void Start()
    {
        rotatingGear.SetActive(false);
    }

    public void ChangeAmmo(int ammo)
    {
        int m = ammo;
        foreach (GameObject go in m_shells)
        {
            go.SetActive(m-- > 0);
        }
        rotatingGear.SetActive(ammo == 0);
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
