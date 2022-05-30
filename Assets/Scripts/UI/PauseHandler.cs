using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs playerInputs;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject pauseBar;
    [SerializeField] private GameObject settingsBar;

    private bool isPaused;

    private void Update()
    {
          if (playerInputs.pause)
            CheckPause();
    }

    private void CheckPause()
    {
        playerInputs.pause = false;
        if (!isPaused)
        {
            UsePause(true, 0);
        }
        else if (isPaused)
            UsePause(false,1);
    }

    private void UsePause(bool isActive, float timeScale)
    {
        pauseScreen.SetActive(isActive);
        if (playerInputs.gameObject.TryGetComponent<PlayerInput>(out PlayerInput input))
            input.enabled = !isActive;
        Time.timeScale = timeScale;
        isPaused = !isPaused;
    }

    public void BackToMenu()
    {
        SceneChanger sceneChanger = new SceneChanger();
        sceneChanger.LoadMainMenuScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Unpause()
    {
        UsePause(false,1);
    }
}
