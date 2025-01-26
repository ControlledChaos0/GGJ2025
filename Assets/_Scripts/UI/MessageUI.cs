using System.Collections;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    private TextMeshProUGUI m_messageText;
    private Animator m_animator;
    private void Awake()
    {
        m_messageText = GetComponent<TextMeshProUGUI>();
        m_animator = GetComponent<Animator>();
    }
    public void DisplayeMessage(string message, float duration = 4f)
    {
        m_messageText.text = message;
        StartCoroutine(MessageAnimationThread(duration));
    }
    private IEnumerator MessageAnimationThread(float duration)
    {
        m_animator.Play("show");
        yield return new WaitForSeconds(duration);
        m_animator.Play("hide");
    }
}
