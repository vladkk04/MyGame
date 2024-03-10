using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 5f;


    [SerializeField]
    private float smooothTimeRotation = 0.1f;

    [SerializeField]
    private float smoothInputSpeed = .2f;

    private float currentVelocity;
    private float velocity;

    private CharacterController characterController;
    private PlayerInputHandler inputHandler;
    private Animator animator;


    private Vector3 inputDirection;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelecity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        currentInputVector = Vector2.SmoothDamp(currentInputVector, inputHandler.MoveInput, ref smoothInputVelecity, smoothInputSpeed);

        inputDirection = new Vector3(currentInputVector.x, 0, currentInputVector.y);

        HandleGravity();
        HandleJumping();

        characterController.Move(inputDirection * speed * Time.deltaTime);

        if (inputHandler.SprintValue > 0)
        {
            animator.SetFloat("Input Magnitude", currentInputVector.magnitude);
        }
        else
        {
            animator.SetFloat("Input Magnitude", currentInputVector.magnitude / 2);
        }

        

    }

    void HandleJumping()
    {
        if (inputHandler.JumpTriggered && characterController.isGrounded)
        {
            animator.SetBool("isJump", true);
            velocity += jumpForce;
        }
        else
        {
            animator.SetBool("isJump", false);

        }

    }

    void HandleRotation()
    {
        if (inputHandler.MoveInput.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smooothTimeRotation);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    void HandleGravity()
    {
        if (characterController.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        inputDirection.y = velocity;
    }

}
