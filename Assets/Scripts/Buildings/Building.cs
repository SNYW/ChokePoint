using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool playerOwned;
    public string buildingName;
    public float maxHealth;
    public float currentHealth;
    public int buildCost;
    public Slider healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.gameObject.SetActive(false);
    }

    public void Die()
    {
        if (playerOwned)
        {
            MapDataManager.playerBuildings.Remove(transform);
        }
        else
        {
            MapDataManager.enemyBuildings.Remove(transform);
        }
        var tile = BuildGridManager.GetTile(transform.position);
        tile.DeleteBuilding();
        Destroy(gameObject);
    }
}
