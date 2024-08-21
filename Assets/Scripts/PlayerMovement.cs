using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, PlayerControls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public event Action JumpEvent;

    PlayerControls controls;    
    CharacterController controller;
    Animator animator;
    public float walkSpeed = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    void OnDestroy()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3();
        movement.x = MovementValue.x;
        movement.y = -9.8f;
        movement.z = MovementValue.y;

        controller.Move(movement * Time.deltaTime * walkSpeed);

        if(MovementValue == Vector2.zero)
        {
            animator.SetFloat("FreeLookSpeed", 0, .1f, Time.deltaTime);
            return;
        }

        animator.SetFloat("FreeLookSpeed", 1, .1f, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(CaluclateDirection());
    }

    Vector3 CaluclateDirection()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * MovementValue.y + right * MovementValue.x;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();         
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }
}
