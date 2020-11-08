using UnityEngine;

public class PlayerUnit : Unit
{
    private CombatSystem combatSystem;
    private MovementSystem movementSystem;
    public GameObject selectIndicator;
    public bool selected;

    void Start()
    {
        selectIndicator.SetActive(false);
        combatSystem = GetComponent<CombatSystem>();
        movementSystem = GetComponent<MovementSystem>();
    }
    void Update()
    {
        selectIndicator.SetActive(selected && GameManager.gm.RTSMode);
        healthBar.value = (currentHealth/maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
        movementSystem.Move();
        combatSystem.ManageAttack();
        if (selected)
        {
            selected = false;
        }
    }
}
