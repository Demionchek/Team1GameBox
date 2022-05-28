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
    private Vector3 throwDirection;
    private bool isAxeThrow;
    private Rigidbody axeRigidBody;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        axeRigidBody = axe.GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        mouseManager = GetComponent<MeleeAtack>().GetMouseManager();
    }

    private void Update()
    {
        CheckAxeThrowState();
    }

    private void CheckAxeThrowState()
    {
        if (!isAxeThrow && input.throwAxe) 
        {
            TryUpdateUi();
            ChangeAxeThrowState();
        }
    }

    private void TryUpdateUi()
    {
        if (axeThrowEvent != null)
            axeThrowEvent.Invoke();
    }

    private void ChangeAxeThrowState()
    {
        throwDirection = mouseManager.GetMousePosition();
        mouseManager.LookAtMouseDirection();
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

    //Called in the middle of Animation
    private void ThrowAxe()
    {
        axe.SetActive(true);
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;
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
        input.throwAxe = false;
        animatorManager.SetAxeThrow(false);
    }
}
