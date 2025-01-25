using UnityEngine;

public class InteractCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Player>().interactables.Add(other.gameObject);
        IInteraction showUI = other.gameObject.GetComponent<IInteraction>();
        if(showUI != null)
        {
            showUI.ShowUI(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<Player>().interactables.Remove(other.gameObject);
        IInteraction showUI = other.gameObject.GetComponent<IInteraction>();
        if (showUI != null)
        {
            showUI.ShowUI(false);
        }
    }

}
