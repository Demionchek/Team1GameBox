using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsActivator : MonoBehaviour
{
    [SerializeField] private SavingSystem _saveSystem;
    [SerializeField] private UnlockItems[] _scrolls;

    void Start()
    {
        if (_scrolls != null)
        {
            foreach (var scroll in _scrolls)
            {
                
            }
        }
    }
}
