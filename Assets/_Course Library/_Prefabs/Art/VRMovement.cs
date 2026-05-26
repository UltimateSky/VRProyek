using UnityEngine;

public class VRMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform head; // Main Camera

    public float speed = 2.0f;
    public float gravity = -9.81f;

    private float yVelocity;

    void Update()
    {
        // ===== 1. IKUTIN POSISI HEAD (BIAR GAK TEMBUS) =====
        Vector3 headPos = new Vector3(head.position.x, transform.position.y, head.position.z);
        Vector3 offset = headPos - transform.position;

        controller.Move(offset);

        // ===== 2. INPUT WASD =====
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Arah gerak sesuai arah kepala
        Vector3 forward = head.forward;
        Vector3 right = head.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * z + right * x;

        // ===== 3. GRAVITY =====
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}