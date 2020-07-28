public class PlayerUnit : Unit
{
    private CombatSystem combatSystem;
    private MovementSystem movementSystem;

    void Start()
    {
        combatSystem = GetComponent<CombatSystem>();
        movementSystem = GetComponent<MovementSystem>();
    }
    void Update()
    {
        healthBar.value = (currentHealth/maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
        movementSystem.Move();
        combatSystem.ManageAttack();
    }
}
