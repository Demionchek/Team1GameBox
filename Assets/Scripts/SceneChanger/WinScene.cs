using UnityEngine;

public class WinScene : MonoBehaviour
{
    private Health health;
    
    private void Start()
    {
        if (TryGetComponent<Health>(out Health health))
            this.health = health;
    }

    void Update()
    {
        if (health.Hp < 1)
            SetWinScene();
    }

    private void SetWinScene()
    {
        SceneChanger changer = new SceneChanger();
        changer.LoadWinScene();
    }
}
