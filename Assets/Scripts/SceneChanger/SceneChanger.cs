using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Saver _saver;

    private const string firstLevelSceneName = "Village";
    private const string mainMenuSceneName = "MainMenu";
    private const string winSceneName = "WinScene";

    private void Start()
    {
        if (_saver.LevelToSave != 0)
        {
            _continueButton.interactable = true;
        }
        else
        {
            _continueButton.interactable = false;
        }
    }

    public void Continue()
    {
        int sceneToLoad = _saver.LevelToSave;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadFirstScene()
    {
        LoadScene(firstLevelSceneName);
    }

    public void LoadMainMenuScene() 
    {
        LoadScene(mainMenuSceneName);
    }

    public void LoadWinScene()
    {
        LoadScene(winSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
