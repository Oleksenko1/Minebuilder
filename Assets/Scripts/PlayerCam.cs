using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;

    [Inject] private PlayerInput playerInput;

    private float xRotation;
    private float yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        Vector2 input = playerInput.GetMouseMovement();

        // Gets input
        float mouseX = input.x * sensX;
        float mouseY = input.y * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
