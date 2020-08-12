using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float zoomRate,panRate,panBorderThickness;
    public Vector3 maxCam;
    public Vector3 minCam;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panRate *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panRate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panRate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panRate * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera main = Camera.main.GetComponent<Camera>();
       // pos.y -= Mathf.Clamp(pos.y scroll * zoomRate * 100 * Time.deltaTime, 0, maxCam.y);
        pos.x = Mathf.Clamp(pos.x, minCam.x, maxCam.x);
        pos.z = Mathf.Clamp(pos.z, minCam.z, maxCam.z);
        transform.position = pos;
    }
}
