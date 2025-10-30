using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool JumpInput { get; private set; }

    private PlayerInputActions _playerInputActions;

    private InputAction _move;
    private InputAction _sprint;
    private InputAction _jump;

    
    #region Built-In Functions
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        _move = _playerInputActions.Move.Move;
        _sprint = _playerInputActions.Move.Sprint;
        _jump = _playerInputActions.Move.Jump;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        
        _move.performed += OnMove;
        _move.canceled += OnMove;

        _sprint.performed += OnSprintPerformed;
        _sprint.canceled += OnSprintCanceled;

        _jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
        
        _move.performed -= OnMove;
        _move.canceled -= OnMove;

        _sprint.performed -= OnSprintPerformed;
        _sprint.canceled -= OnSprintCanceled;

        _jump.performed -= OnJump;
    }

    private void OnDestroy()
    {
        _playerInputActions?.Dispose();
    }
    #endregion
    
    #region Call-Back Functions
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnSprintPerformed(InputAction.CallbackContext context) => SprintInput = true;
    private void OnSprintCanceled(InputAction.CallbackContext context) => SprintInput = false;

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = true;
    }
    #endregion
    
    #region ClearInput

    public void ClearJumpInput()
    {
        JumpInput = false;
    }
    #endregion
}
