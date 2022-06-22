using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    
    private void Start()
    {
        
    }

    public class WorldData
    {
        public int Level;
        public int CheckPoint;
        public int PassedArena;
        public int[] CollectedScrolls;
    }

    public class PlayerData
    {
        public int Health;
        public int Energy;
        public int HealthPacks;
        public int EnergyPacks;
        public int ScrollsCount;
    }
}
