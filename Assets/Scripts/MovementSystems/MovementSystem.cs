using UnityEngine;

public abstract class MovementSystem : MonoBehaviour
{
    protected Unit unit;
    public bool directControl;
    public abstract void Move();
}
