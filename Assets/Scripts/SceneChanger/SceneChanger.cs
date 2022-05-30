using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private const string firstLevelSceneName = "Village";
    private const string mainMenuSceneName = "MainMenu";

    public void LoadFirstScene()
    {
        LoadScene(firstLevelSceneName);
    }

    public void LoadMainMenuScene() 
    {
        LoadScene(mainMenuSceneName);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
