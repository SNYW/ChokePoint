using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour
{
    public PlayerEconomy playerEconomy;
    public Grid grid;
    public Transform buildCursor;
    public SpriteRenderer buildCursorSprite;
    public GameObject selectedBuilding;
    public float buildAreaRadius;
    public LayerMask mask;
    Vector2 mousePosition;
    Vector3Int gridPos;
    public bool deleteMode;
    public GridTile currentTile;

    private int sortingOrderBase = 0;
    private void Start()
    {
        selectedBuilding = null;
        currentTile = null;
        deleteMode = false;
    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = (grid.LocalToCell(mousePosition));
        //Debug.Log(BuildGridManager.gridTiles.ContainsKey(grid.GetCellCenterLocal(gridPos)));
        if (BuildGridManager.gridTiles.ContainsKey(grid.GetCellCenterLocal(gridPos)))
        {
            currentTile = BuildGridManager.GetTile(grid.GetCellCenterLocal(gridPos));
        }
        else
        {
            currentTile = null;
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
                DeleteBuilding(grid.GetCellCenterLocal(gridPos));
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
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridPos = (grid.LocalToCell(mousePosition));
        buildCursor.position = (Vector2)grid.GetCellCenterLocal(gridPos);
        ShowBuildArea();
    }

    public void SetSelectedBuilding(GameObject toBuild)
    {
        this.selectedBuilding = toBuild;
    }
    public void SetCursorImage(Sprite sprite)
    {
       buildCursorSprite.sprite = sprite;
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
        var tile = BuildGridManager.GetTile(grid.GetCellCenterLocal(gridPos));
        if (tile.buildable && playerEconomy.currentResource - selectedBuilding.GetComponent<Building>().buildCost >= 0)
        {
            var building = Instantiate(selectedBuilding, (Vector2)grid.GetCellCenterLocal(gridPos), quaternion.identity);
            var buildingComponent = building.GetComponent<Building>();
            if (buildingComponent.playerOwned)
            {
                MapDataManager.Add(MapDataManager.playerBuildings, building.transform);
            }
            else
            {
                MapDataManager.Add(MapDataManager.enemyBuildings, building.transform);
            }
            building.GetComponentInChildren<SpriteRenderer>().sortingOrder = (sortingOrderBase - gridPos.x) - gridPos.y;
            playerEconomy.currentResource -= building.GetComponent<Building>().buildCost;
            tile.building = building;
            tile.buildable = false;
        }
        else
        {
            ResetBuildState();
        }
    }
    private void HideBuildArea()
    {
        foreach (Vector2 tile in BuildGridManager.gridTiles.Keys)
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

    private void DeleteBuilding(Vector2 position)
    {
        BuildGridManager.GetTile(position).DeleteBuilding();
    }

    public void ResetBuildState()
    {
        selectedBuilding = null;
        HideBuildArea();
    }
}
