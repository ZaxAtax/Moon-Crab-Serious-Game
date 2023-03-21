using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float sensitivity = 100f; // Mouse sensitivity
    public float smoothing = 2f; // Mouse smoothing

    private Vector2 smoothMouseDelta; // Used for mouse smoothing
    private Vector2 currentMouseDelta;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse input
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Smooth the mouse input
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothMouseDelta.x = Mathf.Lerp(smoothMouseDelta.x, mouseDelta.x, 1f / smoothing);
        smoothMouseDelta.y = Mathf.Lerp(smoothMouseDelta.y, mouseDelta.y, 1f / smoothing);

        // Update the current mouse delta
        currentMouseDelta += smoothMouseDelta;

        // Clamp the Y axis rotation
        currentMouseDelta.y = Mathf.Clamp(currentMouseDelta.y, -90f, 90f);

        // Rotate the camera based on the mouse input
        transform.localRotation = Quaternion.AngleAxis(-currentMouseDelta.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentMouseDelta.x, Vector3.up);
    }
}
