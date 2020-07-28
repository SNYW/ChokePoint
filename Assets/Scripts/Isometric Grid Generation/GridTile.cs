using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
public class GridTile : MonoBehaviour
{
    private new SpriteRenderer renderer;
    public bool buildable;
    public Color buildableColour;
    public Color notBuildableColour;
    public GameObject building;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        buildable = true;
        building = null;
    }
    private void Start()
    {
        renderer.enabled = false;
    }

    public void SetActive()
    {
        renderer.enabled = true;
        if (buildable)
        {
            renderer.color = buildableColour;
        }
        else
        {
            renderer.color = notBuildableColour;
        }
    }
    public void SetInactive()
    {
        renderer.enabled = false;
    }

    public void DeleteBuilding()
    {
        if (!buildable)
        {
            buildable = true;
            Destroy(building);
            building = null;
        }
    }
 
}
