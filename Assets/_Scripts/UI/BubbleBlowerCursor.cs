using UnityEngine;

public class BubbleBlowerCursor : Singleton<BubbleBlowerCursor>
{
    // Unity Messages
    private void Awake()
    {
        InitializeSingleton();
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void OnEnable()
    {
        Cursor.visible = false;
    }
    public void OnDisable()
    {
        Cursor.visible = true;
    }
    public void OnDestroy()
    {
        Cursor.visible = true;
    }
    // On Mouse Actions
    public void OnShoot()
    {

    }
    public void OnBlow()
    {

    }
}
