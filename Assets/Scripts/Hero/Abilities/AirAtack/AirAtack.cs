using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(MeleeAtack))]
public class AirAtack : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;

    private AnimatorManager animatorManager;
    private StarterAssetsInputs inputs;
    private Energy energy;
    private bool isAirAtackCooled = true;

    public bool IsAirAtack { get { return !animatorManager.isGrounded() && inputs.atack; } }

    public UnityEvent UpdateUI;

    public static event Action<Vector3, float> CreateMarker;
    private const int hitCount = 15;

    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
        energy = GetComponent<Energy>();
    }

    void Update()
    {
        CheckAirAtack();
    }

    private void CheckAirAtack()
    {
        if (AirAtackAvailable())
        {
            animatorManager.SetAirAtack(IsAirAtack);
        }
        TryUseAirAtack();
    }

    private bool AirAtackAvailable()
    {
        return IsAirAtack && energy.CheckEnergyAvailable(configs.airAtackCost) && isAirAtackCooled;
    }

    private void TryUseAirAtack()
    {
        if (animatorManager.GetAirAtack() && animatorManager.isGrounded())
        {
            energy.UseEnergy(configs.airAtackCost);
            UpdateUI.Invoke();
            animatorManager.SetAirAtack(false);
            AirHit();
            if (CreateMarker != null)
            {
                CreateMarker(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z) + transform.forward, 0.1f);
            }
            StartCoroutine(BlockThrowAxe());
            StartCoroutine(CoolDown());
        }
    }

    private void AirHit()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[hitCount];
        if (Physics.SphereCastNonAlloc(ray, configs.airAtackRange, hits, 0, configs.enemyLayer) > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(configs.mightyPunchDamage, configs.enemyLayer);
                    Vector3 pushVector = hit.transform.position - transform.position;
                    hit.transform.GetComponent<NavMeshAgent>().velocity = pushVector.normalized * configs.mightyPunchForce;
#if (UNITY_EDITOR)
                    Debug.Log($"AirHit {hit.transform.name}");
#endif
                }
            }
        }
    } 

    private IEnumerator CoolDown()
    {
        isAirAtackCooled = false;
        yield return new WaitForSecondsRealtime(configs.airAtackCooldown);
        isAirAtackCooled = true;
    }

    private IEnumerator BlockThrowAxe()
    {
        yield return new WaitForEndOfFrame();
        inputs.throwAxe = false;
    }
}
