using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class MarkerDamageScript : MonoBehaviour
{
    private ThirdPersonController _thirdPersonController;
    private bool _isPlayerInZone;
    private bool _isAClowd;
    private bool _isSlowed;
    private const float kHeigth = 0.001f;
    private float slowRate;
    private float _slowTimer;
    private float _normalPlayerSpeed;

    public void TryToHit(int dmg, LayerMask mask)
    {
        if (_isPlayerInZone)
        {
            _thirdPersonController.GetComponent<IDamageable>().TakeDamage(dmg, mask);
#if(UNITY_EDITOR)
            Debug.Log("PLAYER HITTED " + dmg);
#endif
        }
    }

    public void Slowing(float slowRate, float slowTimer)
    {
        _isAClowd = true;
        this.slowRate = slowRate;
        _slowTimer = slowTimer;
    }

    public void PondResize(float radius)
    {
        transform.localScale = new Vector3(radius, kHeigth, radius);
    }

    public void RayResize(float width, float length)
    {
        transform.localScale = new Vector3(width, length, 1);
    }

    public void ConeResize(float width, float length)
    {
        transform.localScale = new Vector3(width, 1, length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPersonController))
        {
            _thirdPersonController = thirdPersonController;
            _isPlayerInZone = true;
            if (_isAClowd && !_isSlowed)
            {
                _isSlowed = true;
                _normalPlayerSpeed = thirdPersonController.SprintSpeed;
                thirdPersonController.SprintSpeed *= slowRate;
            }
            else if (_isAClowd && _isSlowed)
            {
                StopAllCoroutines();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPersonController))
        {
            _isPlayerInZone = false;
            if (_isAClowd)
            {
                StartCoroutine(SlowTimer(thirdPersonController, _slowTimer));
            }
        }
    }

    private IEnumerator SlowTimer(ThirdPersonController thirdPersonController, float slowTimer)
    {
        yield return new WaitForSeconds(slowTimer);
        thirdPersonController.SprintSpeed /= slowRate;
        _isSlowed = false;
    }

    private void OnDestroy()
    {
        if (_isAClowd && _thirdPersonController != null)
        {
            _thirdPersonController.SprintSpeed = _normalPlayerSpeed;
        }
    }
}
