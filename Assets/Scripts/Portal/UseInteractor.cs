using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class UseInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactWith;
    [SerializeField] private float hitRadius;
    [SerializeField] private float hitDistance;
    [SerializeField] private int countToHit;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    public void TryInteract()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[countToHit];
        if (Physics.SphereCastNonAlloc(ray, hitRadius, hits, hitDistance, _interactWith) > 0)
        {
            Debug.Log("HasHits");
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null && hit.transform.TryGetComponent<IUse>(out IUse use))
                {
                    use.Use(_controller);
#if(UNITY_EDITOR)
                    Debug.Log("use " + hit.transform.name);
#endif
                }
            }
        }
        else
        {
            Debug.Log("NoHits");
        }
    }
}
