using UnityEngine;

public class Pickup : MonoBehaviour, IInteraction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Interact(GameObject player)
    {
        Destroy(gameObject);
        player.GetComponent<Player>().PowerUpIncrease();
    }

   
}
