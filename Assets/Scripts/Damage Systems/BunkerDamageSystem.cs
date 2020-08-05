using UnityEngine;

public class BunkerDamageSystem : DamageSystem
{
    public Bunker bunker;

    private void Start()
    {
        bunker = GetComponent<Bunker>();
    }
    void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        if(bunker.target != null)
        {
            Gizmos.DrawWireSphere(bunker.target.transform.position, bunker.attackArea);
        }
    }
    public override void DealDamage()
    {
        Collider2D[] aoeHit = Physics2D.OverlapCircleAll(bunker.target.transform.position, bunker.attackArea);
        Debug.DrawLine(bunker.transform.position, bunker.target.transform.position);
        foreach (Collider2D enemy in aoeHit)
        {
            var unit = enemy.GetComponent<Unit>();
            if (unit != null && unit.playerOwned != bunker.playerOwned)
            {
                var ds = enemy.GetComponent<DamageSystem>();
                if (ds != null)
                {
                    ds.TakeDamage(bunker.attackDamage);
                }
            }
        }
        
    }

    public override void TakeDamage(int amount)
    {
        bunker.healthbar.gameObject.SetActive(true);
        bunker.currentHealth -= amount;
        if (bunker.currentHealth <= 0) { bunker.Die(); }
        bunker.healthbar.value = bunker.currentHealth / bunker.maxHealth;
    }
}
