using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputController inputController;
    [SerializeField] PlayerStateController stateController;

    [SerializeField] Transform view;
    [SerializeField] float rotationSpeed = 50f;

    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 5f; // Maximum movement speed
    [SerializeField] float agility;

    [SerializeField] float timeToMaxSpeed = 1.5f; // Time to reach max speed, based on agility
    [SerializeField] float timeToStop = 1.5f;

    [SerializeField] Rigidbody rb; // Reference to the Rigidbody

    private Vector2 input; // Input value from the Input System
    private Vector3 currentVelocity; // Current velocity of the player

    private bool startCalled;

    private void Start()
    {
        SubscribeToEvents();
        startCalled = true;

        timeToMaxSpeed = GetTimeFromAgility(agility);
        timeToStop = GetTimeFromAgility(agility);
    }

    private void Update()
    {
        // Rotate the view based on the player's input
        view.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
    }

    private void FixedUpdate()
    {

        if (stateController.IsKnockBacked)
        {
            currentVelocity = rb.velocity;
            return;
        }

        ApplyMovement();   
    }

    private void ApplyMovement()
    {
        Vector3 inputVector = new Vector3(input.x, 0f, input.y);

        // Calculate target velocity based on input
        Vector3 targetVelocity = inputVector * maxSpeed;

        // Calculate acceleration and deceleration
        float acceleration = maxSpeed / timeToMaxSpeed;
        float deceleration = maxSpeed / timeToStop;

        // Apply movement based on input
        if (inputVector.magnitude > 0)
        {
            // Accelerate towards the target velocity
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate towards zero if no input
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply the calculated velocity directly to the Rigidbody (non-physics-based movement)
        rb.MovePosition(transform.position + currentVelocity * Time.fixedDeltaTime);
    }

    private void GetMovementValue(InputAction.CallbackContext context)
    {
        // Update input value based on player input (from the Input System)
        input = context.ReadValue<Vector2>();
    }

    private float GetTimeFromAgility(float agility)
    {
        // Example agility-to-time formula: higher agility means lower time
        return Mathf.Max(0.5f, 2f - (agility * 0.1f));
    }

    private void SubscribeToEvents()
    {
        // Subscribe to input movement events
        inputController.MoveEvent += GetMovementValue;
    }

    private void OnEnable()
    {
        if (startCalled)
        {
            SubscribeToEvents();
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        inputController.MoveEvent -= GetMovementValue;
    }
}
