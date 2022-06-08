using UnityEngine;

public class WinScene : MonoBehaviour
{
    private Health health;
    [SerializeField] private FadeInOut fadeInOut;

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
        fadeInOut.FadeInAndLoadScene(4);
    }
}
