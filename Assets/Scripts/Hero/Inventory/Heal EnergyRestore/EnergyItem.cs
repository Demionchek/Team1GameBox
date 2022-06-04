
public class EnergyItem : Item
{
    public override void Use()
    {
        base.Use();
    }

    public void SetEnergyItem(Inventory inventory)
    {
        inventory.gameObject.TryGetComponent<Health>(out Health health);
        inventory.gameObject.TryGetComponent<Energy>(out Energy energy);
        SetPlayersHealthAndEnergy(health, energy);
    }
}
