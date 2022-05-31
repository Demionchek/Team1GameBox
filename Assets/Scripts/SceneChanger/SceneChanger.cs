using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private const string firstLevelSceneName = "Village";
    private const string mainMenuSceneName = "MainMenu";
    private const string winSceneName = "WinScene";

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
