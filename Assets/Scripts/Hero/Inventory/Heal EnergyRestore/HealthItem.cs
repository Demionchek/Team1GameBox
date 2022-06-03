
public class HealthItem : Item
{
    public override void Use()
    {
        base.Use();
    }

    public void SetHealthItem(Inventory inventory)
    {
        inventory.gameObject.TryGetComponent<Health>(out Health health);
        inventory.gameObject.TryGetComponent<Energy>(out Energy energy);
        SetPlayersHealthAndEnergy(health, energy);
    }
}
