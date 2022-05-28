using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float hpRestore;
    [SerializeField] private float energyRestore;

    private Health playersHealth;
    private Energy playersEnergy;

    public static Action UpdateUi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Inventory>(out Inventory inventory) && PickUp(inventory))
        {
            playersHealth = other.GetComponent<Health>();
            playersEnergy = other.GetComponent<Energy>();
        }
    }

    private bool PickUp(Inventory inventory)
    {
        return inventory.AddItem(this);
    }

    public virtual void Use()
    {
        playersHealth.RestoreHealth(hpRestore);
        playersEnergy.RestoreEnergy(energyRestore);
        UpdateUi?.Invoke();
        Destroy(this, 1f);
    }
}
