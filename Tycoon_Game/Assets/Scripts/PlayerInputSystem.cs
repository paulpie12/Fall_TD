using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 movementInput;

    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("References")]
    [SerializeField] private Transform cam; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Automatically find the main camera if not assigned
        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        // Convert input into camera-relative movement
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // We only want horizontal camera direction
        camForward.y = 0f;
        camRight.y = 0f;

        // Normalize so diagonal movement isn't faster
        camForward.Normalize();
        camRight.Normalize();

        // Calculate move direction relative to camera
        Vector3 move = camForward * movementInput.y + camRight * movementInput.x;

        // Apply force in camera-relative direction
        rb.AddForce(move * speed, ForceMode.Force);

        // Rotate player to face movement direction if moving
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.deltaTime));
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }
}