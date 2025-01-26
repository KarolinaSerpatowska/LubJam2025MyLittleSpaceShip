using UnityEngine;
using UnityEngine.UI;

public class SuperAtak : MonoBehaviour

{
    
    public Slider SliderSpecialAttack;
    public GameObject Sphere;
    public GameObject SphereSpawnPoint;
    public float dist2;

    public GameObject lufa;

    private bool shouldFly = false;

    public MeshRenderer meshRenderer;

float zPos, xPos, yPos;
Vector3 newPos;

    private void Awake()
    {
        meshRenderer.enabled = false;
    }

    private void Start(){
        
        
        Sphere.GetComponent<Renderer>().enabled = false;

        Component[] renderers = Sphere.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }


        Debug.Log(Sphere.GetComponent<Renderer>().enabled);

        yPos = lufa.gameObject.transform.position.y;
xPos = lufa.gameObject.transform.position.x;
zPos = lufa.gameObject.transform.position.z;
//GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
}
bool spacja = false;


private void MoveOnZ(float amount)
{
transform.position -= transform.forward * amount;
}

    private void Powiekszanie()
{
if(transform.localScale.x < 13.0f){
transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
}
}

void Update () {
        float dist = Vector3.Distance(SphereSpawnPoint.transform.position, transform.position);
        if (dist >= dist2)
        {
            Sphere.transform.position = SphereSpawnPoint.transform.position;
            shouldFly = false;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            meshRenderer.enabled = false;
        }


        if (SliderSpecialAttack.value >= 2)
        {
            Sphere.GetComponent<Renderer>().enabled = true;
            //SliderSpecialAttack.value = 0;
        }
//this.gameObject.transform.position.x = lufa.gameObject.transform.position.x;

//this.gameObject.transform.position = newPos;

if (Input.GetKey("space") && Time.timeScale != 0 && SliderSpecialAttack.value >= 2) {
//Debug.Log("abcd");
yPos = lufa.gameObject.transform.position.y;
xPos = lufa.gameObject.transform.position.x;
zPos = lufa.gameObject.transform.position.z;
newPos = new Vector3(xPos,yPos,transform.position.z);
this.gameObject.transform.position = newPos;
GetComponent<Renderer>().enabled = true;
//this.gameObject.SetActive(true);

Powiekszanie();}

if (Input.GetKeyUp("space") && Time.timeScale != 0 && SliderSpecialAttack.value >=2) {
spacja=true;
            shouldFly = true;
            SliderSpecialAttack.value = 0;
        }

if(spacja==true && shouldFly && Time.timeScale !=0){
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
