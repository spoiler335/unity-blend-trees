using System.Data.Common;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();
        if (DI.di.inputManager == null)
        {
            DI.di.SetInputManager(this);
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public bool isJumpClicked => inputActions.Player.Jump.WasPressedThisFrame();
    public bool isAttackClicked => inputActions.Player.Attack.WasPressedThisFrame();

    public float GetForward() => inputActions.Player.Movement.ReadValue<Vector2>().y;
    public float GetRight() => inputActions.Player.Movement.ReadValue<Vector2>().x;

    public Vector2 GetMoveAxis() => inputActions.Player.Movement.ReadValue<Vector2>();
}