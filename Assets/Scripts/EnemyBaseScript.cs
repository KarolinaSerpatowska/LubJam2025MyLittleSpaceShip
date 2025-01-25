using UnityEngine;

public class EnemyBaseScript : MonoBehaviour, IDamagable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   public void TakeDMG(float amount)
    {
        Debug.Log("Take dmg");
    }
}
