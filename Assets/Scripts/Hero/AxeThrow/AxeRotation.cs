using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotation : MonoBehaviour
{
    private float xRotation;

    private void Update()
    {
        if (this.enabled)
            RotateAxe();
    }

    private void RotateAxe()
    {
        Quaternion angle = new Quaternion(transform.rotation.x,50, 90,0f);
        angle.y += 10;
        transform.localRotation = angle;
    }
}
