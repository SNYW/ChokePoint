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
            return MapDataManager.GetClosestTarget(MapDataManager.enemyUnits, transform);
        }
        else
        {
            return MapDataManager.GetClosestTarget(MapDataManager.playerUnits, transform);
        }
       
    }

    public override bool CanSee(Transform target)
    {
        return Vector2.Distance(transform.position, target.position) < unit.aggroRange;
    }
}
