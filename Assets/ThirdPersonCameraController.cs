using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target; // the target object to follow
    public float distance = 5.0f; // distance from target
    public float height = 1.0f; // height of camera
    public float smoothSpeed = 0.5f; // smoothing speed
    public float rotationSpeed = 180.0f; // rotation speed
    public float minDistance = 2.0f; // minimum distance from target
    public float maxDistance = 10.0f; // maximum distance from target

    private float currentDistance;
    private float currentRotationY;
    private float currentRotationX;

    void Start()
    {
        currentDistance = distance;
        currentRotationY = transform.eulerAngles.y;
        currentRotationX = target.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Rotate the character on the x-axis
        currentRotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        target.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

        // Move the camera up and down on the y-axis
        currentRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        currentRotationX = Mathf.Clamp(currentRotationX, -90f, 90f);

        // Use the mouse wheel to scroll the camera in and out
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Set the position and rotation of the camera
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
        Vector3 position = rotation * new Vector3(0.0f, height, -currentDistance) + target.position;

        transform.rotation = rotation;
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed * Time.deltaTime);
    }
}
