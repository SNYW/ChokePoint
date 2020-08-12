using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshMovementSystem : MovementSystem
{
    public LayerMask layerMask;
    private NavMeshAgent navMeshAgent;
    private float floory;

    private void Start()
    {
        unit = GetComponent<Unit>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        floory = GameObject.Find("Floor").transform.position.y;
        if (unit.playerOwned && unit.target == null)
        {
            unit.target = GameObject.Find("EnemyBase");
        }
        else
        {
            unit.target = GameObject.Find("PlayerBase");
        }
    }
    public override void Move()
    {
        if (unit.target != null)
        {
            if (Vector3.Distance(unit.transform.position, unit.target.transform.position) > unit.attackRange)
            {
                var navPosition = new Vector3(unit.target.transform.position.x, floory, unit.target.transform.position.z);
                navMeshAgent.destination = navPosition;
            }
            else
            {
                navMeshAgent.destination = transform.position;
            }
        }
        else
        {
            if (unit.playerOwned)
            {
                unit.target = GameObject.Find("EnemyBase");
            }
            else
            {
                unit.target = GameObject.Find("PlayerBase");
            }
        }
    }
}
