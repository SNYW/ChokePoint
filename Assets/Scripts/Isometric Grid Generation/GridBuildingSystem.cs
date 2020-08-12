using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour
{
    public PlayerEconomy playerEconomy;
    public Grid grid;
    public Transform buildCursor;
    public MeshFilter buildCursorMesh;
    public GameObject selectedBuilding;
    public float buildAreaRadius;
    public LayerMask mask;
    public bool deleteMode;
    public GridTile currentTile;

    private void Start()
    {
        selectedBuilding = null;
        currentTile = null;
        deleteMode = false;
    }
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 1000))
        {
            var selectedObject = hitData.transform.gameObject;
            if (null != selectedObject.GetComponent<GridTile>())
            {
                currentTile = selectedObject.GetComponent<GridTile>();
            }
            else
            {
                currentTile = null;
            }
        }
        if (selectedBuilding != null && currentTile !=null)
        {
            deleteMode = false;
            ShowBuildCursor();
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckBuildArea())
                {
                    Build();
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        ResetBuildState();
                    }
                }
                else if(!CheckBuildArea() && !Input.GetKey(KeyCode.LeftShift))
                {
                    ResetBuildState();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                ResetBuildState();
            }
        }
        else if(deleteMode)
        {
            selectedBuilding = null;
            if(currentTile != null)
            {
                HideBuildArea();
            }
            if (Input.GetMouseButtonDown(0))
            {
                DeleteBuilding(grid.GetCellCenterLocal(grid.WorldToCell(currentTile.transform.position)));
                deleteMode = false;
            }
        }else
        {
            buildCursor.gameObject.SetActive(false);
        }
    }

    private void ShowBuildCursor()
    {
        HideBuildArea();
        buildCursor.gameObject.SetActive(true);
        buildCursor.position = grid.GetCellCenterWorld(grid.WorldToCell(currentTile.transform.position));
        ShowBuildArea();
    }

    public void SetSelectedBuilding(GameObject toBuild)
    {
        this.selectedBuilding = toBuild;
    }
    public void SetCursorImage(Mesh mesh)
    {
       buildCursorMesh.sharedMesh = mesh;
    }

    public void SetDeleteMode()
    {
        ResetBuildState();
        deleteMode = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(buildCursor.position, buildAreaRadius);
    }

    private void Build()
    {
        var tile = currentTile;
        if (tile.buildable && playerEconomy.currentResource - selectedBuilding.GetComponent<Building>().buildCost >= 0)
        {
            var building = Instantiate(selectedBuilding, grid.GetCellCenterWorld(grid.WorldToCell(currentTile.transform.position)), quaternion.identity);
            var buildingComponent = building.GetComponent<Building>();
            if (buildingComponent.playerOwned)
            {
                MapDataManager.Add(MapDataManager.playerBuildings, building.transform);
            }
            else
            {
                MapDataManager.Add(MapDataManager.enemyBuildings, building.transform);
            }
            playerEconomy.currentResource -= building.GetComponent<Building>().buildCost;
            tile.building = building;
            tile.buildable = false;
        }
        else
        {
            Debug.Log("Can't Build on " + currentTile.name + "||"+ tile.buildable);
            ResetBuildState();
        }
    }
    private void HideBuildArea()
    {
        foreach (Vector3 tile in BuildGridManager.gridTiles.Keys)
        {
            BuildGridManager.gridTiles[tile].GetComponent<GridTile>().SetInactive();
        }
    }
    private void ShowBuildArea()
    {
        foreach (GridTile tile in BuildGridManager.GetTileNeighbours(currentTile, 1))
        {
            tile.SetActive();
        }
    }

    private bool CheckBuildArea()
    {
        var canBuild = true;
        foreach (GridTile tile in BuildGridManager.GetTileNeighbours(currentTile, 1))
        {
            if (!tile.buildable)
            {
                canBuild = false;
            }
        }
        return canBuild && !EventSystem.current.IsPointerOverGameObject();
    }

    private void DeleteBuilding(Vector3 position)
    {
        BuildGridManager.GetTile(position).DeleteBuilding();
    }

    public void ResetBuildState()
    {
        selectedBuilding = null;
        HideBuildArea();
    }
}
