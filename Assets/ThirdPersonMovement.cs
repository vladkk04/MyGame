using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private Animator animator;

    public CharacterController characterController;
    public Transform cam;

    [Range(0, 10)]
    [SerializeField] 
    private float speed = 5f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    private float previousMagnitude;
    public float magnitudeTransitionSpeed = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);

        if (inputMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDir.normalized * speed * Time.deltaTime);

            float smoothedMagnitude = Mathf.Lerp(previousMagnitude, inputMagnitude, magnitudeTransitionSpeed * Time.deltaTime * 6);
            previousMagnitude = smoothedMagnitude;
            animator.SetFloat("Input Magnitude", smoothedMagnitude);
        }
        else if(animator.GetFloat("Input Magnitude") > 0)
        {
            // Reset magnitude when not moving
            animator.SetFloat("Input Magnitude", animator.GetFloat("Input Magnitude") - Time.deltaTime * 6);
            previousMagnitude = 0f;
        }
    }
}
