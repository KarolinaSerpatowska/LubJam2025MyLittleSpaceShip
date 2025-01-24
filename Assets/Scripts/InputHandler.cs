using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class InputHandler : MonoBehaviour
{
    private InputManager playerInput;

    private InputAction moveAction;
    private InputAction scrollAction;
    private InputAction mouseAction;
    private InputAction interactAction;
    private InputAction jumpAction;
    private InputAction mouseClick;

    public Vector2 moveInput { get; private set; }
    public float scrollInput { get; private set; }
    public Vector2 mouseInput { get; private set; } 

    public static InputHandler Instance { get; private set; }

    public UnityEvent OnMovement;
    public UnityEvent OnZoom;
    public UnityEvent OnMouseMovement;
    public UnityEvent OnInteract;
    public UnityEvent OnJump;
    public UnityEvent OnMouseClick;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        playerInput = new InputManager();

        moveAction = playerInput.Player.Move;
        scrollAction = playerInput.Player.Scroll;
        mouseAction = playerInput.Player.Mouse;
        interactAction = playerInput.Player.Interact;
        jumpAction = playerInput.Player.Jump;
        mouseClick = playerInput.Player.MouseClick;

        moveAction.performed += OnMovementPerformed;
        moveAction.canceled += OnMovementCanceled;

        scrollAction.performed += OnScrollPerformed;
        mouseAction.performed += OnMouseMove;
        mouseClick.performed += OnMouseClicking;

        interactAction.performed += OnInteraction;

        jumpAction.performed += OnJumping;
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        scrollAction.Enable();
        mouseAction.Enable();
        interactAction.Enable();
        jumpAction.Enable();
        mouseClick.Enable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
        Debug.Log("Movement");
        OnMovement.Invoke();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    { 
        moveInput = Vector2.zero;
        OnMovement.Invoke();
    }

    private void OnScrollPerformed(InputAction.CallbackContext value)
    {
        scrollInput = value.ReadValue<float>();
        if (scrollInput >= 1f) scrollInput = 1f;
        else if(scrollInput <= -1f) scrollInput = -1f;
        else scrollInput = 0f;
        OnZoom.Invoke();
    }

    private void OnMouseMove(InputAction.CallbackContext value)
    {
        mouseInput = value.ReadValue<Vector2>();
        OnMouseMovement.Invoke();
    }

    private void OnInteraction(InputAction.CallbackContext value)
    {
        OnInteract.Invoke();
    }

    private void OnJumping(InputAction.CallbackContext context)
    {
        OnJump.Invoke();
    }

    private void OnMouseClicking(InputAction.CallbackContext context)
    {
        OnMouseClick.Invoke();
    }
}
