using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour
{
    private InputManager playerInput;

    private InputAction moveAction;
    private InputAction scrollAction;
    private InputAction mouseAction;
    private InputAction interactAction;
    private InputAction pauseAction;
    private InputAction fireAction;

    public Vector2 moveInput { get; private set; }
    public float scrollInput { get; private set; }
    public Vector2 mouseInput { get; private set; } 

    public static InputHandler Instance { get; private set; }

    public UnityEvent OnMovement;
    public UnityEvent OnZoom;
    public UnityEvent OnMouseMovement;
    public UnityEvent OnInteract;
    public UnityEvent OnMouseClick;
    public UnityEvent OnPauseAction;
    public UnityEvent OnFireAction;
    public UnityEvent OnFireHold;

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

        pauseAction = playerInput.Player.Pause;
        fireAction = playerInput.Player.Fire;

        moveAction.performed += OnMovementPerformed;
        moveAction.canceled += OnMovementCanceled;

        scrollAction.performed += OnScrollPerformed;
        mouseAction.performed += OnMouseMove;

        interactAction.performed += OnInteraction;

        pauseAction.performed += OnPausePerformed;
        fireAction.performed += OnFirePerformed;

        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        scrollAction.Enable();
        mouseAction.Enable();
        interactAction.Enable();
        pauseAction.Enable();
        fireAction.Enable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
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

    private void OnPausePerformed(InputAction.CallbackContext value)
    {
        OnPauseAction.Invoke();
    }

    private void OnFirePerformed(InputAction.CallbackContext value)
    {
       if( value.interaction is PressInteraction ) OnFireAction.Invoke();
       else if(value.interaction is HoldInteraction) OnFireHold.Invoke();
    }
}
