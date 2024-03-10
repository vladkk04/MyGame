using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private string actionMapName = "Player";
    [SerializeField] private string move = "Movement";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string throwing = "Throwing";
    [SerializeField] private string pickUp = "PickUp";


    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction; 

    private InputAction throwingAction; 
    private InputAction pickUpAction; 

    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool ThrowingTriggered { get; private set; }
    public bool PickUpTriggered { get; private set; }
    public float SprintValue { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);

        throwingAction = playerControls.FindActionMap(actionMapName).FindAction(throwing);
        pickUpAction = playerControls.FindActionMap(actionMapName).FindAction(pickUp);

        RegisterInputAction();
    }

    void RegisterInputAction()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        throwingAction.performed += context => ThrowingTriggered = true;
        throwingAction.canceled += context => ThrowingTriggered = false;

        pickUpAction.performed += context => PickUpTriggered = true;
        pickUpAction.canceled += context => PickUpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        throwingAction.Enable();
        pickUpAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        throwingAction.Disable();
        pickUpAction.Disable();
        sprintAction.Disable();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
