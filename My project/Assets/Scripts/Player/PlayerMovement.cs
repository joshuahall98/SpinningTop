using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputController inputController;
    [SerializeField] PlayerStateController stateController;

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

        Vector3 inputVector = new Vector3(input.x, 0f, input.y); // Convert input to X-Z plane

        // Calculate target velocity
        Vector3 targetVelocity = inputVector * maxSpeed;

        // Calculate acceleration and deceleration dynamically
        float acceleration = maxSpeed / timeToMaxSpeed;
        float deceleration = maxSpeed / timeToStop;

        // Handle acceleration and deceleration
        if (inputVector.magnitude > 0)
        {
            // Apply acceleration toward the target velocity
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Apply deceleration toward zero when no input is present
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply velocity to the Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
    }

    private void GetMovementValue(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private float GetTimeFromAgility(float agility)
    {
        // Example agility-to-time formula: higher agility means lower time
        return Mathf.Max(0.5f, 2f - (agility * 0.1f));
    }

    private void SubscribeToEvents()
    {
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
        inputController.MoveEvent -= GetMovementValue;
    }
}
