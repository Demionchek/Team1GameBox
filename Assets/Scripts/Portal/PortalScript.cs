using System.Collections;
using UnityEngine;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour , IUse
{
    [SerializeField] private Transform _teleportationTarget;
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private bool _isNextLevel;
    [SerializeField] private int _nextLevelNum;
    private bool _isCooled = true;

    public void Use(CharacterController controller)
    {
        if (_isCooled && !_isNextLevel)
        {
            Debug.Log("Used");
            controller.enabled = false;
            controller.transform.position = _teleportationTarget.position;
            controller.enabled = true;
            _isCooled = false;
            StartCoroutine(CoolDown());
        }

        if (_isCooled && _isNextLevel)
        {
            SceneManager.LoadScene(_nextLevelNum);

        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        _isCooled = true;
    }
}
