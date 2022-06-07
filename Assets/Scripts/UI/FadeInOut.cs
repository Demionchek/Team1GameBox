using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private Animator animator;
    private readonly int fadeOut = Animator.StringToHash("FadeOut");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartFade()
    {
        animator.SetTrigger(fadeOut);
    }
}
