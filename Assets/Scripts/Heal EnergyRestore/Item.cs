using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<StarterAssets.StarterAssetsInputs>() != null)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        // ���� �������� � ���������� � ��������� ���� ���� ����, ���� ��� �� ��� � �����
    }
}
