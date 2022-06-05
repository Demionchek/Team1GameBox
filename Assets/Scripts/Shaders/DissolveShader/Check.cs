using UnityEngine;

public class Check : MonoBehaviour
{
    [SerializeField] private Health health;
    private DissolveObject dissolve;

    private void Start()
    {
        if (TryGetComponent<Health>(out Health health))
            this.health = health;
        if (TryGetComponent<DissolveObject>(out DissolveObject dissolve))
            this.dissolve = dissolve;
    }

    void Update()
    {
        if (health.Hp < 1)
            dissolve.StartDissolve();
    }
}
