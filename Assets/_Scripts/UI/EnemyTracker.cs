using TMPro;
using UnityEngine;

public class EnemyTracker : Panel
{
    [SerializeField] private TextMeshProUGUI m_enemyCount;
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public void UpdateText(string s)
    {
        m_enemyCount.text = s;
    }
}
