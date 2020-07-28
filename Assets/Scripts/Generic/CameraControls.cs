using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float maxOutZoom,maxInZoom,zoomRate,panRate,panBorderThickness;
    public Vector2 maxCam;
    public Vector2 minCam;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panRate *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panRate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panRate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panRate * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera main = Camera.main.GetComponent<Camera>();
        main.orthographicSize -= scroll * zoomRate * 100 * Time.deltaTime;
        main.orthographicSize = Mathf.Clamp(main.orthographicSize, maxInZoom, maxOutZoom);

        pos.x = Mathf.Clamp(pos.x, minCam.x, maxCam.x);
        pos.y = Mathf.Clamp(pos.y, minCam.y, maxCam.y);
        transform.position = pos;
    }
}
