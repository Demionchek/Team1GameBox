using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private Tween fadeTween;

    public void Start()
    {
       canvasGroup.alpha = 1;
       FadeOut(1.5f);
    }

    public void FadeInAndLoadScene(int index)
    {
        var duration = 2f;
        FadeIn(duration);
        StartCoroutine(WaitAndLoadScene(index,duration));
    }

    private IEnumerator WaitAndLoadScene(int index,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(index);
    }

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }    

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if(fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
