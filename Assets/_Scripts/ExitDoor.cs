using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            LevelManager.Instance.TransitionToNextScene();
        }
    }
}
