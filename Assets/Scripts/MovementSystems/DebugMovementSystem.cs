using UnityEngine;

public class DebugMovementSystem : MovementSystem
{
    private Transform defaultTargetPosition;
    private new Unit unit;
    private void Start()
    {
        unit = GetComponent<Unit>();
        if (unit.playerOwned)
        {
            defaultTargetPosition = GameObject.Find("EnemyBase").transform;
        }
        else
        {
            defaultTargetPosition = GameObject.Find("PlayerBase").transform;
        }
        
    }
    public override void Move()
    {
        if(unit.target == null)
        {
          transform.position = Vector3.MoveTowards(transform.position, defaultTargetPosition.position, unit.moveSpeed * Time.deltaTime) ;
        }
        else
        {
            if(Vector3.Distance(transform.position, unit.target.transform.position) > unit.attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, unit.target.transform.position, unit.moveSpeed * Time.deltaTime);
            }
        }
    }
}
