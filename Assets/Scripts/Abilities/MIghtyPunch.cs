using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Energy))]
public class MIghtyPunch : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject prefab;

    private StarterAssetsInputs playerInputs;
    private AnimatorManager animatorManager;
    private Energy energy;

    private bool isMightyPunchCooled = true;

    void Start()
    {
        playerInputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<MeleeAtack>().GetAnimatorManager();
        energy = GetComponent<Energy>();
    }

    void Update()
    {
        CheckMightyPunch();
    }

    private void CheckMightyPunch()
    {
        if (isMightyPunchAvailable())
        {
            Punch();
        }
    }

    private bool isMightyPunchAvailable()
    {
        return playerInputs.mightyPunch && energy.CheckEnergyAvailable(configs.mightyPunchCost)
            && isMightyPunchCooled && animatorManager.isGrounded();
    }

    private void Punch()
    {
        energy.UseEnergy(configs.mightyPunchCost);
        StopMovement();
        animatorManager.SetMightyPunch(true);
        StartCoroutine(ShowPunchZone());
        StartCoroutine(CoolDown());
    }

    private IEnumerator ShowPunchZone()
    {
        prefab.SetActive(true);
        yield return new WaitForSeconds(1f);
        prefab.SetActive(false);
    }

    private IEnumerator CoolDown()
    {
        isMightyPunchCooled = false;
        yield return new WaitForSeconds(configs.mightyPunchCooldown);
        playerInputs.mightyPunch = false;
        isMightyPunchCooled = true;
    }

    private void StopMovement()
    {
        playerInputs.move = Vector2.zero;
    }

    public void ResetMightyPunch()
    {
        playerInputs.mightyPunch = false;
        animatorManager.SetMightyPunch(false);
    }
}
