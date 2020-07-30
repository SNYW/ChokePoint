using UnityEngine;

public class DebugCombatSystem : CombatSystem
{
    private Unit unit;
    private DamageSystem damageSystem;
    private float attackCooldown;
    private void Start()
    {
        unit = GetComponent<Unit>();
        damageSystem = GetComponent<DamageSystem>();
        attackCooldown = unit.attackSpeed;
    }
    public override void ManageAttack()
    {
        if (GetClosestTarget() != null && CanSee(GetClosestTarget().transform) && GetClosestTarget().activeSelf)
        {
            unit.target = GetClosestTarget();
            if (CanAttack(unit.target.transform) && unit.target != null)
            {
                attackCooldown = unit.attackSpeed;
                Attack();
            }
        }
        else
        {
            unit.target = null;
        }
    }
    public override void Attack()
    {
        damageSystem.DealDamage();
    }

    public override bool CanAttack(Transform target)
    {
        attackCooldown -= Time.deltaTime;
        return attackCooldown <= 0 && Vector2.Distance(transform.position, target.position) < unit.attackRange;
    }

    public override GameObject GetTarget()
    {
        return GetClosestTarget();
    }

    private GameObject GetClosestTarget()
    {
        if (unit.playerOwned)
        {
            var closestUnit =  MapDataManager.GetClosestTarget(MapDataManager.enemyUnits, transform);
            var closestBuilding = MapDataManager.GetClosestTarget(MapDataManager.enemyBuildings, transform);
            return ClosestBetween(closestBuilding, closestUnit);
        }
        else
        {
            var closestUnit =  MapDataManager.GetClosestTarget(MapDataManager.playerUnits, transform);
            var closestBuilding = MapDataManager.GetClosestTarget(MapDataManager.playerBuildings, transform); 
            return ClosestBetween(closestBuilding, closestUnit);
        }
       
    }

    private GameObject ClosestBetween(GameObject obj1, GameObject obj2)
    {
        if (obj1 != null && obj2 != null)
        {
            if (Vector2.Distance(transform.position, obj1.transform.position) < Vector2.Distance(transform.position, obj2.transform.position))
            {
                return obj1;
            }
            else
            {
                return obj2;
            }
        }
        else
        {
            if (obj1 == null)
            {
                return obj2;
            }
            else
            {
                return obj1;
            }
        }
    }

    public override bool CanSee(Transform target)
    {
        return Vector2.Distance(transform.position, target.position) < unit.aggroRange;
    }
}
