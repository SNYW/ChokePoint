using UnityEngine;

public class Bunker : Building
{
    public float range;
    public int attackDamage;
    public float attackCooldown;
    public float attackArea;
    public GameObject target;
    private CombatSystem combatSystem;

    private void Start()
    {
        if (playerOwned)
        {
            MapDataManager.Add(MapDataManager.playerBuildings, transform);
        }
        else
        {
            MapDataManager.Add(MapDataManager.enemyBuildings, transform);
        }
        combatSystem = GetComponent<CombatSystem>();
    }

    private void Update()
    {
        combatSystem.ManageAttack();
    }

}
