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

    //to reset player rotation
    private void resetRotationState()
    {        
        //input.atack = false;
        mousePositionManager.StopLookingAtMouseDirection();
        animatorManager.ResetBackwardRun();
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        animatorManager.SetAtack(false);
    }
    
    public MousePositionManager GetMouseManager() 
        => this.mousePositionManager;
}
