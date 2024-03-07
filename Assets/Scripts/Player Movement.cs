using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;

    private Vector2 moveDirection = Vector2.zero;

    [Range(0, 20)]
    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveDirection.x, 0, moveDirection.y);
        rb.MovePosition(transform.position + direction * Time.deltaTime * speed);
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Movement.performed += OnMovementPerformed;
        playerInput.Player.Movement.canceled += OnMovementCancelled;

    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Movement.performed -= OnMovementPerformed;
        playerInput.Player.Movement.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveDirection = Vector2.zero;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    
}
