public class BunkerDamageSystem : DamageSystem
{
    private Bunker bunker;

    private void Start()
    {
        bunker = GetComponent<Bunker>();
    }
    public override void DealDamage()
    {
        var ds = bunker.target.GetComponent<DamageSystem>();
        if (ds != null)
        {
            ds.TakeDamage(bunker.attackDamage);
        }
    }

    public override void TakeDamage(int amount)
    {
        bunker.healthbar.gameObject.SetActive(true);
        bunker.TakeDamage(amount);
    }
}
