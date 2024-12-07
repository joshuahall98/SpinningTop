using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputController inputController;
    [SerializeField] PlayerStateController stateController;

    [SerializeField] Transform view;
    [SerializeField] Rigidbody rb; // Reference to the Rigidbody
    [SerializeField] float rotationSpeed = 50f;

    [Header("Movement Settings")]
    [SerializeField, Range(15f, 25f)] float maxSpeed = 5f; // Maximum movement speed
    [SerializeField, Range(0.25f, 0.75f)] float movementResponseTime;

    [Header("Tilt Settings")]
    [SerializeField] float leanAmount = 10f; // Maximum lean angle in degrees
    [SerializeField] float leanSpeed = 5f;  // Speed of leaning adjustment

    private Vector2 input; // Input value from the Input System
    private Vector3 currentVelocity; // Current velocity of the player 
    public Vector3 CurrentVelocity => currentVelocity;

    private bool startCalled;

    private void Start()
    {
        SubscribeToEvents();
        startCalled = true;
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
        ApplyLeaning();
    }

    private void ApplyMovement()
    {
        Vector3 inputVector = new Vector3(input.x, 0f, input.y);

        // Calculate target velocity based on input
        Vector3 targetVelocity = inputVector * maxSpeed;

        // Calculate acceleration and deceleration
        float agility = maxSpeed / movementResponseTime; 

        // Apply movement based on input
        if (inputVector.magnitude > 0)
        {
            // Accelerate towards the target velocity
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, agility * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate towards zero if no input
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, agility * Time.fixedDeltaTime);
        }

        // Apply the calculated velocity directly to the Rigidbody (non-physics-based movement)
        rb.MovePosition(transform.position + currentVelocity * Time.fixedDeltaTime);
    }

    private void GetMovementValue(InputAction.CallbackContext context)
    {
        // Update input value based on player input (from the Input System)
        input = context.ReadValue<Vector2>();
    }

    private void ApplyLeaning()
    {
        // If there's no movement, don't lean
        if (currentVelocity.magnitude < 0.1f)
        {
            ResetLeaning();
            return;
        }

        // Get the normalized movement direction in local space
        Vector3 localDirection = transform.InverseTransformDirection(currentVelocity.normalized);

        // Calculate lean based on forward and sideways movement
        float leanAngleZ = -localDirection.x * leanAmount; // Side-to-side tilt (banking)
        float leanAngleX = localDirection.z * leanAmount;  // Forward/backward tilt

        // Create a target rotation for the lean
        Quaternion targetLean = Quaternion.Euler(leanAngleX, 0f, leanAngleZ);

        // Smoothly interpolate the current rotation to the target lean rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLean, leanSpeed * Time.fixedDeltaTime);
    }

    private void ResetLeaning()
    {
        // Smoothly return to upright position
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, leanSpeed * Time.fixedDeltaTime);
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
