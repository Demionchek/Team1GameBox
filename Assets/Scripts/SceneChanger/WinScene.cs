using UnityEngine;

public class WinScene : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeInOut;

    public void SetWinScene()
    {
        fadeInOut.FadeInAndLoadScene(4);
    }
}
