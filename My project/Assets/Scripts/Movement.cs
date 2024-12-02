using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] InputController controller;

    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 5f; // Maximum movement speed
    [SerializeField] float agility;

    [SerializeField] float timeToMaxSpeed = 1.5f; // Time to reach max speed, based on agility
    [SerializeField] float timeToStop = 1.5f;

    Vector2 input;
    private Vector3 inputVector; // Input value from the Input System
    private Vector3 currentVelocity; // Current velocity of the player

    bool startCalled;

    private void Start()
    {
        SubscribeToEvents();
        startCalled = true;

        timeToMaxSpeed = GetTimeFromAgility(agility);
        timeToStop = GetTimeFromAgility(agility);
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(input.x, 0f, input.y); // Convert input to X-Z plane

        // Calculate target velocity
        Vector3 targetVelocity = inputVector * maxSpeed;

        // Calculate acceleration and deceleration dynamically
        float acceleration = maxSpeed / timeToMaxSpeed;
        float deceleration = maxSpeed / timeToStop;

        // Handle acceleration and deceleration
        if (inputVector.magnitude > 0)
        {
            // Apply acceleration toward the target velocity
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.deltaTime
            );
        }
        else
        {
            // Apply deceleration toward zero when no input is present
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.deltaTime
            );
        }

        // Move the player
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
    }

    private void GetMovementValue(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Debug.Log($"Input: {input}");
    }
    private float GetTimeFromAgility(float agility)
    {
        // Example agility-to-time formula: higher agility means lower time
        // Adjust the curve to fit your game's design
        return Mathf.Max(0.5f, 2f - (agility * 0.1f));
    }


    private void SubscribeToEvents()
    {
        controller.MoveEvent += GetMovementValue;
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
        controller.MoveEvent -= GetMovementValue;
    }
}
