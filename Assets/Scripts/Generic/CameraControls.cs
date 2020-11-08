using System;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float zoomRate,panRate,panBorderThickness;
    public Vector3 maxCam;
    public Vector3 minCam;

    public Vector3 mobaOffsets;

    void Update()
    {
        if (GameManager.gm.RTSMode)
        {
            MoveCameraRTS();
        }
        else
        {
            MoveCameraMoba();
        }
    }

    public void MoveCameraRTS()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panRate * Time.deltaTime;
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

        pos.x = Mathf.Clamp(pos.x, minCam.x, maxCam.x);
        pos.z = Mathf.Clamp(pos.z, minCam.z, maxCam.z);
        pos.y = 13.6f;
        transform.position = pos;
    }
    
    public void MoveCameraMoba()
    {
        var targetpos = GameManager.gm.selectedUnit.transform.position;
        var offsetpos = targetpos + mobaOffsets;
        transform.position = new Vector3(offsetpos.x, offsetpos.y, offsetpos.z);
    }
}
