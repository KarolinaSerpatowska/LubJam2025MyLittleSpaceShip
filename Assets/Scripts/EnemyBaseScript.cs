using UnityEngine;

public class EnemyBaseScript : MonoBehaviour, IDamagable
{
    public GameObject particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   public void TakeDMG(float amount)
    {
        Debug.Log("Take dmg");
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Pickup")
        {
            if(other.GetComponent<pocisk>() && other.GetComponent<pocisk>().ownerTag != gameObject.tag)
            {
                GetComponent<AudioSource>().Play();
                particle.SetActive(true);
            }
            
        }
       

    }
}
