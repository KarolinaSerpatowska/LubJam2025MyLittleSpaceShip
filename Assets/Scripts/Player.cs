using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour, IDamagable
{
    private Rigidbody rb;

    [SerializeField] private Vector2 moveInput;
    private Vector3 input;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float forwardSpeed = 5;

    public List<GameObject> interactables;

    private int powerUpAmount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InputHandler.Instance.OnMovement.AddListener(UpdateMovement);
        InputHandler.Instance.OnInteract.AddListener(InteractButton);
        InputHandler.Instance.OnFireAction.AddListener(Fire);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateMovement()
    {
        moveInput = InputHandler.Instance.moveInput;
        input = new Vector3(-moveInput.x, moveInput.y, 0).normalized * moveSpeed;
    }

    private void Move()
    {
        Vector3 forward = -transform.forward.normalized * forwardSpeed;
        input.z = forward.z;
        rb.MovePosition(transform.position + input * Time.fixedDeltaTime);
    }

    private void InteractButton()
    {
        if (interactables.Count != 0)
        {
            IInteraction interactable = interactables[0].GetComponent<IInteraction>();
            if (interactable != null)
            {
                interactable.Interact(this.gameObject);
                interactables.RemoveAt(0);
            }
        }

        
    }

    public void PowerUpIncrease()
    {
        powerUpAmount += 1;
        Debug.Log(powerUpAmount);
    }

    private void Fire()
    {
        Debug.Log("FIRE");
    }

    public void TakeDMG(float amount)
    {
        Debug.Log("Take dmg");
    }
}
