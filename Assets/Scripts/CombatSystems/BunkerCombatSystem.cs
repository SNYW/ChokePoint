using UnityEngine;

public class BunkerCombatSystem : CombatSystem
{
    private Bunker bunker;
    private DamageSystem damageSystem;
    private float attackCooldown;

    private void Start()
    {
        bunker = GetComponent<Bunker>();
        damageSystem = GetComponent<BunkerDamageSystem>();
        attackCooldown = bunker.attackCooldown;
    }

    public override void Attack()
    {
        damageSystem.DealDamage();
    }

    public override void ManageAttack()
    {
        if (GetClosestTarget() != null && CanSee(GetClosestTarget().transform) && GetClosestTarget().activeSelf)
        {
            bunker.target = GetClosestTarget();
            if (CanAttack(bunker.target.transform) && bunker.target != null)
            {
                attackCooldown = bunker.attackCooldown;
                Attack();
            }
        }
        else
        {
            bunker.target = null;
        }
    }
    public override bool CanAttack(Transform target)
    {
        attackCooldown -= Time.deltaTime;
        return attackCooldown <= 0 && Vector3.Distance(transform.position, target.position) < bunker.range;
    }

    protected GameObject GetClosestTarget()
    {
        if (bunker.playerOwned)
        {
            var closestUnit = MapDataManager.GetClosestTarget(MapDataManager.enemyUnits, transform);
            var closestBuilding = MapDataManager.GetClosestTarget(MapDataManager.enemyBuildings, transform);
            return ClosestBetween(closestBuilding, closestUnit);
        }
        else
        {
            var closestUnit = MapDataManager.GetClosestTarget(MapDataManager.playerUnits, transform);
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
        return Vector3.Distance(transform.position, target.position) < bunker.range;
    }

    public override GameObject GetTarget()
    {
        return GetClosestTarget();
    }
}
