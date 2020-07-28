using UnityEngine;

public abstract class CombatSystem : MonoBehaviour
{

    public abstract void ManageAttack();
    public abstract GameObject GetTarget();
    public abstract bool CanAttack(Transform target);
    public abstract bool CanSee(Transform target);
    public abstract void Attack();
}
