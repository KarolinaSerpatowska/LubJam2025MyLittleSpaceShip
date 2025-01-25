using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public GameObject followTarget;

    public float smoothTime = 0.25f;

    private Vector3 currentVelocity;

    public float zoomSpeed = 3f;
    public float clampZoomMax = 30f;
    public float clampZoomMin = -125f;

    private float yaw = 180.0f;
    private float pitch = 0.0f;

    public float rotationSpeed = 0.1f;
    public float minPitch = -60.0f;
    public float maxPitch = 60.0f;

    void Start()
    {
        InputHandler.Instance.OnMouseMovement.AddListener(MoveCamera);
        InputHandler.Instance.OnZoom.AddListener(Zoom);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
       transform.position = Vector3.SmoothDamp(followTarget.transform.position, followTarget.transform.position , ref currentVelocity, smoothTime);
    }

    private void MoveCamera()
    {
        if (Time.timeScale != 0)
        {
            Debug.Log(yaw);
            yaw += rotationSpeed * InputHandler.Instance.mouseInput.x;
            pitch -= rotationSpeed * InputHandler.Instance.mouseInput.y;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    private void Zoom()
    {
        if (Time.timeScale != 0)
        {
            float R = InputHandler.Instance.scrollInput * 15 * zoomSpeed;          //The radius from current camera
            float PosX = mainCamera.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (mainCamera.transform.eulerAngles.y - 90);       //Get left to right
            PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
            PosY = PosY / 180 * Mathf.PI;                                       //^
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
            float Y = R * Mathf.Cos(PosX);                                      //^
            float CamX = mainCamera.transform.position.x;                      //Get current camera postition for the offset
            float CamY = mainCamera.transform.position.y;                      //^
            float CamZ = mainCamera.transform.position.z;                      //^
            float CamZlocal = mainCamera.transform.localPosition.z;
            if (CamZlocal + Z >= clampZoomMin && CamZlocal + Z <= clampZoomMax)
            {
                mainCamera.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
            }
        }
    }
}
