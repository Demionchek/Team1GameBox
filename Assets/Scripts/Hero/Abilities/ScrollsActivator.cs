using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsActivator : MonoBehaviour
{
    [SerializeField] private UnlockItems[] _scrolls;

    void Start()
    {
        SavingSystem savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        savingSystem.LoadWorldData(ref worldData);

        if (_scrolls != null && worldData.CollectedScrolls != null)
        {
            foreach (var scroll in _scrolls)
            {
                foreach(int id in worldData.CollectedScrolls)
                {
                    if (id == scroll.GetInstanceID())
                    {
                        scroll.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
