using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strzelanie : MonoBehaviour
{
        public GameObject GameObjectToSpawn;
    private GameObject Clone;
    public float timeToSpawn = 4f;
    public float FirstSpawn = 10f;


    // Update is called once per frame
    void Update()
    {
        FirstSpawn -= Time.deltaTime;
        if (Input.GetKey("space"))
        {
		//Debug.Log("abcd");
            Clone = Instantiate(GameObjectToSpawn, transform.position, Quaternion.identity);
            FirstSpawn = timeToSpawn;

        }
    }
}
