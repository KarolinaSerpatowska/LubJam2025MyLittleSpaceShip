using UnityEngine;

public class pocisk : MonoBehaviour
{

    //private float speed = 2f;
    public string ownerTag;

    private void MoveOnZ(float amount)
{
transform.position -= transform.forward * amount;
}

void Update () {
MoveOnZ(1.0f);

}

private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != ownerTag && other.gameObject.tag != "Pickup")
            Destroy(other.gameObject, 0.5f); 
    }
}