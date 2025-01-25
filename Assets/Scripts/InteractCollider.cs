using UnityEngine;

public class InteractCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponentInParent<Player>().interactables.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponentInParent<Player>().interactables.Remove(other.gameObject);

    }

}
