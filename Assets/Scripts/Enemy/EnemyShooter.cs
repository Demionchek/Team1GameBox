using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private LayerMask _mask;

    private const float k_targetHeightCorrection = 1f;

    public void Shoot( Vector3 target, int damage)
    {
        target.y += k_targetHeightCorrection;
        GameObject newBullet = Instantiate(_bullet, _shootPosition.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetDamageAndMask(damage, _mask);
        var rb = newBullet.GetComponent<Rigidbody>();
        Vector3 pushVector = target - _shootPosition.position;
        rb.AddForce(pushVector.normalized * _bulletSpeed, ForceMode.Impulse);
        Destroy(newBullet, 2.5f);
    }
}
