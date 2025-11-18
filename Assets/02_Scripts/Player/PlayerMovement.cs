using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header(" - Locomotion Settings - ")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform rayOriginTransform;
    [SerializeField] Vector2 rotationLimitX = new Vector2(-45f, 45f);


    private void OnEnable()
    {
        InputManager.OnMoveInput += InputManager_OnMoveInput;
        InputManager.OnLookInput += InputManager_OnLookInput;
    }
    private void OnDisable()
    {
        InputManager.OnMoveInput -= InputManager_OnMoveInput;
        InputManager.OnLookInput -= InputManager_OnLookInput;
    }

    public void Init(float walkSpeed)
    {
        this.walkSpeed = walkSpeed;
    }

    private void InputManager_OnMoveInput(Vector2 vector)
    {
        Vector3 velocity = Vector3.zero;
        velocity += rayOriginTransform.forward * vector.y;
        velocity += rayOriginTransform.right * vector.x;
        velocity.y = 0;
        velocity.Normalize();
        velocity *= walkSpeed * Time.deltaTime;

        transform.position += velocity;
    }
    private void InputManager_OnLookInput(Vector2 vector)
    {
        Vector3 eulerAngle = rayOriginTransform.rotation.eulerAngles;
        eulerAngle += new Vector3(-vector.y * rotationSpeed * Time.deltaTime, vector.x * rotationSpeed * Time.deltaTime, 0);
        eulerAngle.x = Mathf.Clamp(eulerAngle.x > 180 ? eulerAngle.x - 360 : eulerAngle.x, rotationLimitX.x, rotationLimitX.y);
        rayOriginTransform.rotation = Quaternion.Euler(eulerAngle);
    }

}
