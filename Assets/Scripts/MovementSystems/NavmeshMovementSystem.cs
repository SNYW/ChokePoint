using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshMovementSystem : MovementSystem
{
    public LayerMask layerMask;
    private NavMeshAgent navMeshAgent;
    private float floory;
    public float directMovespeed;
    public bool test;

    private void Start()
    {
        directControl = false;
        unit = GetComponent<Unit>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        floory = GameObject.Find("Floor").transform.position.y;
        ResetTarget();
    }

    public override void Move()
    {
        test = unit.target == null;
        if (!directControl)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            if (unit.target != null)
            {
                if (GetComponent<NavMeshAgent>().enabled)
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
            }
            else
            {
                ResetTarget();
            }
            
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = false;
            ResetTarget();
            Vector3 pos = transform.position;

            if (Input.GetKey(KeyCode.W))
            {
                pos.z += directMovespeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                pos.x -= directMovespeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                pos.z -= directMovespeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                pos.x += directMovespeed * Time.deltaTime;
            }

            transform.position = pos;
        }
    }

    private void ResetTarget()
    {if(unit.target == null)
        {
            if (unit.playerOwned)
            {
                unit.target = GameObject.Find("EnemyBase");
            }
            else
            {
                unit.target = GameObject.Find("PlayerBase");
            }

            var navPosition = new Vector3(unit.target.transform.position.x, floory, unit.target.transform.position.z);
            if (navMeshAgent.enabled)
            {
                navMeshAgent.destination = navPosition;
            }
        }
    }
}
