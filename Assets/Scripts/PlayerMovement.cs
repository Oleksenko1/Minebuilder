using UnityEngine;
using Zenject;
using System;
public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> OnMoveModeChanged;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float inAirMovementDecreseMult = 0.2f;
    [SerializeField] private float delayForJumpFlySwitch = 1f;
    
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Ground Check")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] LayerMask whatIsGround;

    private bool isGrounded;
    private bool isFlying = false;

    [Inject]
    private PlayerInput playerInput;

    private Vector3 moveDirection;

    private float lastPressedJump = 0f;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        PlayerInput.OnSpacebarPressed += Jump;
        PlayerInput.OnSpacebarHeld += FlyUp;
        PlayerInput.OnShiftHeld += FlyDown;
    }
    private void Update()
    {
        CheckForGround();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void CheckForGround()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Handle drag
        if (isGrounded && !isFlying)
            rb.drag = groundDrag;
        else if(!isGrounded && !isFlying)
            rb.drag = 1;
    }
    private void FlyUp()
    {
        if(isFlying)
        {
            rb.AddForce(Vector3.up * moveSpeed, ForceMode.Force);
        }
    }
    private void FlyDown()
    {
        if(isFlying)
        {
            rb.AddForce(Vector3.down * moveSpeed, ForceMode.Force);
        }
    }
    private void Jump()
    {
        // Enables or disabled fly mode
        if (Time.time - lastPressedJump < delayForJumpFlySwitch) ChangeMoveMode();
        
        lastPressedJump = Time.time;

        // Jump
        if (isGrounded && !isFlying)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void ChangeMoveMode()
    {
        isFlying = !isFlying;
        rb.useGravity = !rb.useGravity;
        rb.drag = 2; // Drag in fly mode

        OnMoveModeChanged?.Invoke(isFlying);
    }
    private void MovePlayer()
    {
        Vector2 inputVector = playerInput.GetMovementInput().normalized;

        moveDirection = (orientation.forward * inputVector.y + orientation.right * inputVector.x);

        Vector3 forceAmount = moveDirection.normalized * moveSpeed * 10;
        
        // Decrease movement amount if in the air and not flying
        if(!isFlying)
            forceAmount = isGrounded? forceAmount : forceAmount * inAirMovementDecreseMult;

        rb.AddForce(forceAmount, ForceMode.Force);

        ControllSpeed();
    }
    private void ControllSpeed() // Clamps player's speed
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
