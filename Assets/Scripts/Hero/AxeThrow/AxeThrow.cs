using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeleeAtack))]
public class AxeThrow : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private Transform hand;
    [SerializeField] private float throwPower;
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private UnityEvent axeThrowEvent;

    private AnimatorManager animatorManager;
    private MousePositionManager mouseManager;
    private StarterAssetsInputs input;

    private Rigidbody axeRigidBody;
    private Vector3 throwDirection;
    private bool isAxeThrow;
    private AxeReturn isAxeReturn;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        axeRigidBody = axe.GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        mouseManager = GetComponent<MeleeAtack>().GetMouseManager();
        isAxeReturn = axe.GetComponent<AxeReturn>();
    }

    private void Update()
    {
        CheckAxeThrowState();
    }

    private void CheckAxeThrowState()
    {
        if (ThrowAxe()) 
        {
            TryUpdateUi();
            ChangeAxeThrowState();
        }
    }

    private bool ThrowAxe()
    {
        Debug.Log($"!isAxeThrow {!isAxeThrow}");
        Debug.Log($"isAxeReturn {!isAxeReturn.isActive}");
        return !isAxeThrow && input.throwAxe && !isAxeReturn.isActive;
    }

    private void TryUpdateUi()
    {
        axeThrowEvent?.Invoke();
    }

    private void ChangeAxeThrowState()
    {
        throwDirection = mouseManager.GetMousePosition();
        animatorManager.CheckBackwardRun();
        animatorManager.SetAxeThrow(true);
        StartCoroutine(ThrowCoolDown());
    }

    private IEnumerator ThrowCoolDown()
    {
        isAxeThrow = true;
        yield return new WaitForSeconds(configs.axeThrowCooldown);
        resetThrowAxeState();
        isAxeThrow = false;
    }

    public void LookAtThrowDirection()
    {
        mouseManager.LookAtMouseDirection();
    }

    public void SmoothLookAtThrowDirection()
    {
        mouseManager.SmoothLookAtMouseDirection();
    }

    //Called in the middle of Animation
    private void ThrowAxeEvent()
    {
        if (axe.TryGetComponent<AxeReturn>(out AxeReturn axeReturn))
            axeReturn.isActive = true;

        axe.SetActive(true);
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;

        if (throwDirection.y < hand.position.y && animatorManager.isGrounded())
            throwDirection.y = hand.position.y;
        axe.transform.LookAt(throwDirection);
        Vector3 direction = (throwDirection - axe.transform.position).normalized;
        axeRigidBody.AddForce(direction * throwPower, ForceMode.Impulse);
    }

    //Called after ThrowAxe event
    public void UpdateAxe()
    {
        resetThrowAxeState();
        mouseManager.StopLookingAtMouseDirection();
        animatorManager.ResetBackwardRun();
    }

    private void resetThrowAxeState()
    {
        animatorManager.SetAxeThrow(false);
    }
}
