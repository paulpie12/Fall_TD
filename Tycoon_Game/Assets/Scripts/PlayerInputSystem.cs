using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 movementInput;
    [SerializeField] private float speed = 5f; 

    private void Awake()
    {
         rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        rb.AddForce(move * speed, ForceMode.Force);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed) { 
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
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
