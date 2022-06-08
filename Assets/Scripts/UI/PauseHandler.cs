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
    private bool isSettings;

    private void Update()
    {
          if (playerInputs.pause)
            CheckPause();
    }

    private void CheckPause()
    {
        if (!isPaused)
        {
            Pause();
        }
        else if (isPaused)
            Unpause();
    }

    private void UsePause(bool isActive, float timeScale)
    {
        playerInputs.PauseInput(false);
        pauseScreen.SetActive(isActive);
        Time.timeScale = timeScale;
        isPaused = !isPaused;
    }

    public void ChangeSettingsBar()
    {
        if(!isSettings)
        {
            UseSettingsBar(isSettings);
        }
        else
        {
            UseSettingsBar(isSettings);
        }
        isSettings = !isSettings;
    }

    private void UseSettingsBar(bool isSettings)
    {
        pauseBar.SetActive(isSettings);
        settingsBar.SetActive(!isSettings);
    }

    public void BackToMenu()
    {
        Unpause();
        SceneChanger sceneChanger = new SceneChanger();
        sceneChanger.LoadMainMenuScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        UsePause(true, 0);
        playerInputs.isPaused = true;
    }

    public void Unpause()
    {
        if(isSettings)
            ChangeSettingsBar();  
        UsePause(false,1);
        playerInputs.isPaused = false;
        playerInputs.atack = false;
    }
}
