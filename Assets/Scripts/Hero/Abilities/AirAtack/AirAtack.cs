using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(MeleeAtack))]
public class AirAtack : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject markerPrefab;

    private AnimatorManager animatorManager;
    private StarterAssetsInputs inputs;
    private Energy energy;
    private bool isAirAtackCooled = true;
    public bool IsAirAtack { get { return !animatorManager.isGrounded() && inputs.atack; } }

    private const int hitCount = 15;

    public UnityEvent UpdateUI;

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
            StartCoroutine(PondCorutine(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), 0.1f));
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

    ///
    private IEnumerator PondCorutine(Vector3 pos, float timeToDel)
    {
        GameObject pond = Instantiate(markerPrefab, pos, Quaternion.identity);
        float delay = timeToDel / 3;
        var pondMaterial = pond.GetComponent<Renderer>().material;
        MaterialSetAlfa(pondMaterial, Color.green);
        yield return new WaitForSeconds(delay);
        MaterialSetAlfa(pondMaterial, Color.yellow);
        yield return new WaitForSeconds(delay);
        MaterialSetAlfa(pondMaterial, Color.red);
        yield return new WaitForSeconds(delay);
        Destroy(pond);
    }

    private Material MaterialSetAlfa(Material material, Color color)
    {
        Color newAlfa = new Color(0, 0, 0, 0.5f);
        material.color = color - newAlfa;
        return material;
    }
    ///

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
