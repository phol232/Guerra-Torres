using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public bool IsPrimaryAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool IsSpecialAttack()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}