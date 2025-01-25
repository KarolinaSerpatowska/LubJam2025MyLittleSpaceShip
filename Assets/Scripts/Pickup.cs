using UnityEngine;

public class Pickup : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject interactUI;

    private void Start()
    {
        interactUI.SetActive(false);
    }

    public void Interact(GameObject player)
    {
        Destroy(gameObject);
        player.GetComponent<Player>().PowerUpIncrease();
    }

    public void ShowUI(bool enable)
    {
        if (enable)
        {
            interactUI.SetActive (true);
        }
        else
        {
            interactUI.SetActive(false);
        }
    }
   
}
