using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class AirAtack : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private float effectDelTimer = 0.3f;
    [SerializeField] private GameObject airAtackUi;

    private AnimatorManager animatorManager;
    private StarterAssetsInputs inputs;
    private PlayerEffects playerEffects;
    private Energy energy;
    private PlayerSounds playerSounds;
    private bool isAirAtackCooled = true;

    private const int hitCount = 15;

    public UnityEvent UpdateUI;

    void Start()
    {
        playerSounds = GetComponent<PlayerSounds>();
        inputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
        energy = GetComponent<Energy>();
        playerEffects = GetComponent<PlayerEffects>();
        airAtackUi.SetActive(true);
    }

    void Update()
    {
        CheckAirAtack();
    }

    private void CheckAirAtack()
    {
        if (AirAtackAvailable() && inputs.groundSlam)
        {
            animatorManager.SetAirAtack(true);
            TryUseAirAtack();
        }
    }

    public bool AirAtackAvailable()
    {
        return   energy.CheckEnergyAvailable(configs.airAtackCost) && isAirAtackCooled;
    }

    private void TryUseAirAtack()
    {
        if (animatorManager.GetAirAtack() && animatorManager.isGrounded())
        {
            energy.UseEnergy(configs.airAtackCost);
            UpdateUI.Invoke();

            inputs.groundSlam = false;
            
            StartCoroutine(BlockThrowAxe());
            StartCoroutine(CoolDown());
        }
    }

    private void AirHit()
    {
        playerSounds.PlayAirHitSound();
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
                   if(hit.transform.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
                        agent.velocity = pushVector.normalized * configs.mightyPunchForce;
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
    
    public void AirAtackHit()
    {
        animatorManager.SetAirAtack(false);
        var slamEffect = Instantiate(playerEffects.GroundSlam, transform.position, Quaternion.identity);
        slamEffect.Play();
        Destroy(slamEffect, effectDelTimer);
        AirHit();
    }

    private IEnumerator CoolDown()
    {
        isAirAtackCooled = false;
        yield return new WaitForSecondsRealtime(configs.airAtackCooldown);
        isAirAtackCooled = true;
        playerSounds.AirAttackCDSound();
    }

    private IEnumerator BlockThrowAxe()
    {
        yield return new WaitForEndOfFrame();
        inputs.throwAxe = false;
    }
}
