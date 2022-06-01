using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private int energy;
    public float CurrentEnergy { get; set; }

    private void Start()
    {
        CurrentEnergy = energy;
    }
    public bool CheckEnergyAvailable(float abilitiyCost)
    {
        return CurrentEnergy >= abilitiyCost;
    }

    public void UseEnergy(int cost) 
    {
        CurrentEnergy -= cost;
    }

    public void RestoreEnergy(int value)
    {
        CurrentEnergy += value;
        CurrentEnergy = Mathf.Min(CurrentEnergy, energy);
    }
}
