using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs),typeof(AnimatorManager))]

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] private MousePositionManager mousePositionManager;

    private StarterAssetsInputs input;
    private AnimatorManager animatorManager;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if (input.atack && animatorManager.isGrounded())
        {
            animatorManager.SetAtack(true);
            mousePositionManager.SmoothLookAtMouseDirection();
            animatorManager.CheckBackwardRun();
        }
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackStates()
    {
        animatorManager.SetAtack(false);        
        mousePositionManager.StopLookingAtMouseDirection();
        animatorManager.ResetBackwardRun();
    }
    
    public MousePositionManager GetMouseManager() 
        => this.mousePositionManager;
}
