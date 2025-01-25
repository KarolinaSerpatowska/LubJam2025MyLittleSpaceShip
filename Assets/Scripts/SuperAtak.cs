using UnityEngine;

public class SuperAtak : MonoBehaviour

{
public GameObject lufa;

float zPos, xPos, yPos;
Vector3 newPos;

private void Start(){
yPos = lufa.gameObject.transform.position.y;
xPos = lufa.gameObject.transform.position.x;
zPos = lufa.gameObject.transform.position.z;
GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;}
bool spacja = false;


private void MoveOnZ(float amount)
{
transform.position -= transform.forward * amount;
}

    private void Powiekszanie()
{
if(transform.localScale.x < 13.0f){
transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
}
}

void Update () {

//this.gameObject.transform.position.x = lufa.gameObject.transform.position.x;

//this.gameObject.transform.position = newPos;

if (Input.GetKey("space") && Time.timeScale != 0) {
//Debug.Log("abcd");
yPos = lufa.gameObject.transform.position.y;
xPos = lufa.gameObject.transform.position.x;
zPos = lufa.gameObject.transform.position.z;
newPos = new Vector3(xPos,yPos,transform.position.z);
this.gameObject.transform.position = newPos;
GetComponent<Renderer>().enabled = true;
//this.gameObject.SetActive(true);

Powiekszanie();}

if (Input.GetKeyUp("space") && Time.timeScale != 0) {
spacja=true;}

if(spacja==true && Time.timeScale !=0){
MoveOnZ(0.3f);}
}

void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.parent.gameObject.tag == "a") {
        //Debug.Log("abcd");
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Pickup" && collision.gameObject.tag != "Interact")
        {
            Destroy(collision.gameObject, 0.5f);
        }
            
	//Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Pickup" && other.gameObject.tag != "Interact")
        {
            Destroy(other.gameObject, 0.5f);
        }
           
    }
}
