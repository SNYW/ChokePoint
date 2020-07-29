using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingButton : MonoBehaviour
{
    public GridBuildingSystem gridBuildingSystem;
    public GameObject building;
    public Color buildableColour;
    public Color unbuildableColour;
    public Sprite cursorSprite;
    private Image button;
    private bool buildable;
    private int cost;

    void Start()
    {
        cost = building.GetComponent<Building>().buildCost;
        button = GetComponent<Image>();
    }

    void Update()
    {
        buildable = gridBuildingSystem.playerEconomy.currentResource >= cost;
        ManageColour();
    }

    private void ManageColour()
    {
        if(buildable)
        {
            button.color = buildableColour;
        }
        else
        {
            button.color = unbuildableColour;
        }
    }

    public void Select()
    {
        if (buildable)
        {
            gridBuildingSystem.SetSelectedBuilding(building);
            gridBuildingSystem.SetCursorImage(cursorSprite);
        }
    }
}
