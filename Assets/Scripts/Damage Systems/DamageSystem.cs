using UnityEngine;

public abstract class DamageSystem : MonoBehaviour
{
    public abstract void DealDamage();
    public abstract void TakeDamage(int amount);
}
