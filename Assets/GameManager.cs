using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public bool playerBuilding;
    public bool RTSMode;
    public bool MOBAMode;
    public LayerMask selectMask;

    public GameObject selectedUnit;

    private void Start()
    {
        playerBuilding = false;
        RTSMode = true;
        MOBAMode = false;
        selectedUnit = null;
        if(gm == null)
        {
            gm = this;
        }
    }

    private void Update()
    {
        if(selectedUnit != null && selectedUnit.GetComponent<PlayerUnit>() != null)
        {
            selectedUnit.GetComponent<PlayerUnit>().selected = true;
        }
        HandleUnitHover();
        HandleUnitSelection();
        SetCameraMode();
    }


    private void HandleUnitHover()
    {
        RaycastHit hit;
        Ray select = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(select, out hit, Mathf.Infinity, selectMask))
        {
            hit.collider.GetComponent<PlayerUnit>().selected = true;
        }
    }

    private void HandleUnitSelection()
    {
        if (!playerBuilding && Input.GetMouseButtonDown(0) && GameManager.gm.RTSMode)
        {
            RaycastHit hit;
            Ray select = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("click");
            if (Physics.Raycast(select, out hit, Mathf.Infinity, selectMask))
            {
                Debug.Log("hit");
                selectedUnit = hit.collider.gameObject;
            }
            else
            {
                selectedUnit = null;
            }
        }
    }

    private void SetCameraMode()
    {
        if(selectedUnit != null)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (MOBAMode)
                {
                    selectedUnit.GetComponent<MovementSystem>().directControl = false;
                    selectedUnit = null;
                }
                else
                {
                    selectedUnit.GetComponent<MovementSystem>().directControl = true;
                }

                MOBAMode = !MOBAMode;
                RTSMode = !RTSMode;
            }
        }
        else
        {
            RTSMode = true;
            MOBAMode = false;
        }
    }


}
