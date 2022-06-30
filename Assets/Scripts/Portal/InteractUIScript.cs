using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUIScript : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;


    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeUIState(bool newState)
    {
        if (m_MeshRenderer != null)
        {
           m_MeshRenderer.enabled = newState;

        }
        
    }
}
