using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    private StarterAssetsInputs input;
    private Animator animator;
    [SerializeField] private AudioSource AttackSound;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Atack();
      

    }

    private void Atack()
    {
        if (input.atack)
        {
            animator.SetBool("Atack", true);
            AttackSound.Play();
        }
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        input.atack = false;
        animator.SetBool("Atack", false);
    }
}
 