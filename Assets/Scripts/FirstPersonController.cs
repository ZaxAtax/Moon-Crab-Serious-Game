using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 75.0f;
    public float sprintSpeed = 125.0f;
    public float jumpSpeed = 50.0f;
    public float jumpHeight = 10f;
    private CharacterController controller;
    private float gravity = -200f;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintSpeed;
        }
        else
        {
            movement *= speed;
        }

        movement = transform.TransformDirection(movement);

        if (controller.isGrounded)
        {
            // Jump if the jump button is pressed
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            // Apply gravity to the vertical velocity
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Apply vertical velocity to movement
        movement.y = verticalVelocity;

        // Move the character controller
        controller.Move(movement * Time.deltaTime);
    }
}
