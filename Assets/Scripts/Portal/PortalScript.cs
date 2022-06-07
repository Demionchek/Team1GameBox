using System.Collections;
using UnityEngine;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour, IUse
{
    [SerializeField] private GameObject UIobj;
    [SerializeField] private Transform _teleportationTarget;
    [SerializeField] private Saver _saver;
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private bool _isNextLevel;
    [SerializeField] private int _nextLevelNum;
    private bool _isCooled = true;

    public void Use(CharacterController controller)
    {
        if (_isCooled && !_isNextLevel)
        {
            controller.enabled = false;
            controller.transform.position = _teleportationTarget.position;
            controller.enabled = true;
            _isCooled = false;
            StartCoroutine(CoolDown());
        }

        if (_isCooled && _isNextLevel)
        {
            if (_nextLevelNum != 3)
            {
             _saver.SaveCheckPoint(0);
            }
            int health = (int)controller.GetComponent<Health>().Hp;
            _saver.SaveHealth(health);
            int energy = (int)controller.GetComponent<Energy>().CurrentEnergy;
            _saver.SaveEnergy(energy);
            SceneManager.LoadScene(_nextLevelNum);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            if (UIobj != null)
            {
                UIobj.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            if (UIobj != null)
            {
                UIobj.SetActive(false);
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        _isCooled = true;
    }
}
