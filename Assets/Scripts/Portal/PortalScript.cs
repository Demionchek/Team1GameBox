using System.Collections;
using UnityEngine;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour, IUse
{
    [SerializeField] private GameObject UIobj;
    [SerializeField] private Transform _teleportationTarget;
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private bool _isNextLevel;
    [SerializeField] private int _nextLevelNum;

    [SerializeField] private FadeInOut fadeinOutScreen;

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
                ClearCheckPoints();
            }
            try
            {
                StartFade(_nextLevelNum);
                SavePlayerData(controller);
            }
            catch
            {
                Debug.Log("Cant fade in to next level");
            }
        }
    }

    private void SavePlayerData(CharacterController controller)
    {
        SavingSystem savingSystem = new SavingSystem();
        PlayerData playerData = new PlayerData();
        savingSystem.SavePlayerData(controller);
    }

    private void ClearCheckPoints()
    {
        SavingSystem savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        savingSystem.LoadWorldData(ref worldData);
        savingSystem.SaveWorldData(worldData.Level, 0, worldData.PassedArena, worldData.CollectedScrolls);
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

    private void StartFade(int sceneIndex)
    {
        fadeinOutScreen.FadeInAndLoadScene(sceneIndex);
    }
}
