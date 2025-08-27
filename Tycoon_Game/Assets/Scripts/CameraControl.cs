using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;    // Drag your player here
    [SerializeField] private Transform cam;       // Drag Main Camera here (optional)

    [Header("Camera Settings")]
    [SerializeField] private float distance = 5f;       // Distance behind the player
    [SerializeField] private float height = 2f;         // Height above the player
    [SerializeField] private float sensitivity = 200f;  // Mouse sensitivity
    [SerializeField] private float smoothSpeed = 10f;   // How quickly camera follows

    private float yaw = 0f;   // Horizontal angle
    private float pitch = 15f; // Vertical angle

    void Start()
    {
        // Automatically grab the main camera if left empty
        if (cam == null && Camera.main != null) cam = Camera.main.transform;
        if (cam == null)
        {
            Debug.LogWarning("ThirdPersonCamera: No camera assigned and no Camera.main found.");
            cam = transform;
        }

        // Lock the cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize yaw and pitch based on current camera rotation
        Vector3 angles = cam.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void FixedUpdate()
    {
        if (player == null || cam == null) return;

        // 1) Read mouse input for camera rotation
        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -20f, 60f); // Limit vertical rotation

        // 2) Rotate the player to match the camera's yaw (always show back of player)
        player.rotation = Quaternion.Euler(0f, yaw, 0f);

        // 3) Calculate desired camera position behind the player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
        Vector3 desiredPosition = player.position + Vector3.up * height + offset;

        // 4) Smoothly move the camera to the desired position
        cam.position = Vector3.Lerp(cam.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 5) Always look at the player
        cam.LookAt(player.position + Vector3.up * height);
    }
}
