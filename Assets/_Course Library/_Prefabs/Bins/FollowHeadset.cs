using UnityEngine;

public class FollowHeadsetVR : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float followSpeed = 5f;

    void Update()
    {
        Vector3 targetPosition = new Vector3(
            cameraTransform.position.x,
            transform.position.y,
            cameraTransform.position.z
        );

        Vector3 move = targetPosition - transform.position;

        characterController.Move(move * followSpeed * Time.deltaTime);
    }
}