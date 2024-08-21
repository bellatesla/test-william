using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic movement functions for moving a player transform.
/// </summary>
public class GeneralMovement : MonoBehaviour
{
    public Transform playerCamera;
    public bool isRigidBody;
    PlayerControls controls;
    public enum LookAtStyle { CameraForward, MovementDirection }

    public LookAtStyle lookAtStyle = LookAtStyle.CameraForward;
    public float moveSpeed = 1;
    public float rotationSpeed = 10;
    public ForceMode forceMode;
    public Vector3 input;
    public Vector3 movementDirection;

    bool canJump;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        controls = new PlayerControls();
        controls.Enable();

    }
    void Update()
    {
        input.x = controls.Player.Move.ReadValue<Vector2>().x;
        input.z = controls.Player.Move.ReadValue<Vector2>().y;       
    }

    private void Move()
    {        

        // Get direction from input relative to camera
        movementDirection = playerCamera.TransformDirection(input);
        movementDirection.y = 0;
        //movementDirection.Normalize();// Do not use since inpit is smoothed

        // Rotation direction   
        Vector3 lookAt = movementDirection;

        // lookAt value if using forward
        if (lookAtStyle == LookAtStyle.CameraForward)
        {
            lookAt = playerCamera.forward;
        }

        lookAt.y = 0;
        lookAt.Normalize();

        if (input != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), rotationSpeed * Time.deltaTime);
        }

        // Can give targetDir x and z to animator input here...

        // Movement direction in world space
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
    }
    private void FixedUpdate()
    {
        // Get direction from input relative to camera
        movementDirection = playerCamera.TransformDirection(input);
        movementDirection.y = 0;
        movementDirection.Normalize();
        // Rotation
        Vector3 lookAt = movementDirection;

        // lookAt value if using forward
        if (lookAtStyle == LookAtStyle.CameraForward)
        {
            lookAt = playerCamera.forward;
        }

        lookAt.y = 0;
        lookAt.Normalize();

        if (input != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), rotationSpeed * Time.fixedDeltaTime);
        }

        // Can give targetDir x and z to animator input here...

        // Movement direction in world space
        //transform.Translate(movementDirection * moveSpeed * Time.fixedDeltaTime, Space.World);

        var rb = GetComponent<Rigidbody>();

        //rb.velocity = movementDirection * moveSpeed * Time.fixedDeltaTime;
        rb.AddForce(movementDirection * moveSpeed, forceMode);
    }

    private void Jump()
    {
        throw new System.NotImplementedException();
    }

   


    private void OnDrawGizmos()
    {
        //keyboard input
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + input);
        
        //draw box at movement direction
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position + movementDirection, Vector3.one * .1f);
        
        
        // Relative Vector to Camera
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + movementDirection);

        
    }
}
