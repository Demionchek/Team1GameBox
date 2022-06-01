using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotation : MonoBehaviour
{
    private Rigidbody rigidboy;
    private void Start()
    {
        rigidboy = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (enabled && rigidboy.isKinematic == false)
            gameObject.transform.Rotate(Vector3.right, 10f);
    }
}
