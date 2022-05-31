using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private Saver _saver;

    public void PlayersDeath()
    {
        if (TryGetComponent<AnimatorManager>(out AnimatorManager animatorManager) && TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            animatorManager.SetDeadAnimation(true);
            input.enabled = false;
            StartCoroutine(DeathScreen());
        }
    }

    private IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(0.1f);
        _deathScreen.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(_saver.LevelToSave);
    }
}
