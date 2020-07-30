using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public bool playerOwned;
    public string unitName;
    public float moveSpeed;
    public float maxHealth;
    public float currentHealth;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float aggroRange;
    public Slider healthBar;
    public GameObject target;
    private CombatSystem combatSystem;
    private MovementSystem movementSystem;

    void Start()
    {
        combatSystem = GetComponent<CombatSystem>();
        movementSystem = GetComponent<MovementSystem>();
    }
    private void OnEnable()
    {
        Reset();
    }
    void Update()
    {
        movementSystem.Move();
        combatSystem.ManageAttack();
    }

    public void Die()
    {
        if (playerOwned)
        {
            MapDataManager.playerUnits.Remove(this.transform);
        }
        else
        {
            MapDataManager.enemyUnits.Remove(this.transform);
        }
        MapDataManager.poolDictionary[unitName].Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void Reset()
    {
        currentHealth = maxHealth;
    }
}
