using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public static event Action OnPlacePressed;
    public static event Action OnBreakPressed;
    public static event Action OnSpacebarPressed;
    public static event Action OnSpacebarHeld;
    public static event Action OnShiftHeld;
    public static event Action<KeyCode> OnHotbarPressed;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 mouseMovement = Vector2.zero;
    void Update()
    {
        HandleMouseButtons();

        HandleMovementInput();

        HandleMouseMovement();

        HandleSpacebarPress();

        HandleHotbarButtons();

        HandleShiftPress();
    }
    private void HandleMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y);
    }
    private void HandleMouseMovement()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        mouseMovement = new Vector2(x, y);
    }
    private void HandleMouseButtons()
    {
        // Invokes on right mouse button clicked
        if (Input.GetMouseButtonDown(1))
            OnPlacePressed?.Invoke();

        // Invokes on left mouse button clicked
        if (Input.GetMouseButtonDown(0))
            OnBreakPressed?.Invoke();
    }
    private void HandleSpacebarPress()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            OnSpacebarPressed?.Invoke();

        if (Input.GetKey(KeyCode.Space))
            OnSpacebarHeld?.Invoke();
    }
    public Vector2 GetMovementInput()
    {
        return moveInput;
    }
    public Vector2 GetMouseMovement()
    {
        return mouseMovement;
    }
    private void HandleHotbarButtons()
    {
        KeyCode pressedButton = KeyCode.None;

        // Checks if any hotbar buttons is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1)) { pressedButton = KeyCode.Alpha1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { pressedButton = KeyCode.Alpha2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { pressedButton = KeyCode.Alpha3; }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { pressedButton = KeyCode.Alpha4; }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { pressedButton = KeyCode.Alpha5; }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) { pressedButton = KeyCode.Alpha6; }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) { pressedButton = KeyCode.Alpha7; }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) { pressedButton = KeyCode.Alpha8; }
        else if (Input.GetKeyDown(KeyCode.Alpha9)) { pressedButton = KeyCode.Alpha9; }

        if(pressedButton != KeyCode.None)
        {
            OnHotbarPressed?.Invoke(pressedButton);
        }
    }

    private void HandleShiftPress()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            OnShiftHeld?.Invoke();
        }
    }
}
