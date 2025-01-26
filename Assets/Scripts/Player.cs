using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour, IDamagable
{
    public Slider SliderSpecialAttack;

    private Rigidbody rb;

    [SerializeField] private Vector2 moveInput;
    private Vector3 input;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float forwardSpeed = 5;

    public List<GameObject> interactables;

    private int powerUpAmount = 0;
    [SerializeField] private int powerUpWhenRealese = 2;

    public GameObject GameObjectToSpawn;
    private GameObject Clone;
    public float timeToSpawn = 4f;
    public float FirstSpawn = 10f;

    public float hp = 100.0f;

    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip fireSound2;

    [SerializeField] private GameObject deathScreen;

    public GameObject lufa;
    float zPos, xPos, yPos;
    Vector3 newPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InputHandler.Instance.OnMovement.AddListener(UpdateMovement);
        InputHandler.Instance.OnInteract.AddListener(InteractButton);
        InputHandler.Instance.OnFireAction.AddListener(Fire);
        InputHandler.Instance.OnFireHold.AddListener(SuperFire);
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
        if (input.x < 0)
        {
            transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 10.0f);
        }
        else if (input.x > 0) transform.Rotate(-Vector3.forward * Time.fixedDeltaTime * 10.0f);
        else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.forward), 10.0f * Time.fixedDeltaTime);
    }

    private void InteractButton()
    {
        if (interactables.Count != 0)
        {
            IInteraction interactable = interactables[0].GetComponent<IInteraction>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
                interactables.RemoveAt(0);
            }
        }

        
    }

    public void PowerUpIncrease()
    {
        powerUpAmount += 1;
        Debug.Log(powerUpAmount);
        SliderSpecialAttack.value = powerUpAmount;
    }

    private void Fire()
    {
        if(Time.timeScale != 0)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if(audioSource != null)
            {
                int rand = Random.Range(0, 2);
                if(rand == 0) audioSource.clip = fireSound;
                else audioSource.clip = fireSound2;
                audioSource.Play();
            }

            FirstSpawn -= Time.deltaTime;
            Clone = Instantiate(GameObjectToSpawn, transform.position, Quaternion.identity);
            Clone.GetComponent<pocisk>().ownerTag = gameObject.tag;

            //if(powerUpAmount >= powerUpWhenRealese) //shoot super projectile
            //{
            //    powerUpAmount = 0;
            //}
            //else //shoot normal
            //{

            //}
        }

    }

    private void SuperFire()
    {
        if (powerUpAmount >= powerUpWhenRealese)
        {
            
        }
        Debug.Log("Super fire");
    }

    public void TakeDMG(float amount)
    {
        hp-=amount;
        if(hp <= 0)
        {
            Debug.Log("death");
            Death();
        }
    }

    private void Death()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }
}
