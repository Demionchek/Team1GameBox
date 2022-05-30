using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Hp/Energy Bar")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image energyBar;

    [Header("Abilities")]
    [SerializeField] private Image mightyPunchImage;
    [SerializeField] private Image axeThrowImage;
    [SerializeField] private Image airAtackImage;   
    [SerializeField] private Image dashImage;

    [Header("Inentory")]
    [SerializeField] private Image[] HealPotionSlots;
    [SerializeField] private Image[] EnergyPotionSlots;
    [SerializeField] private Sprite emptyInventorySprite;
    [SerializeField] private Sprite fullInventorySprite;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] PlayerAbilitiesConfigs configs;

    private void OnEnable()
    {
        Inventory.UpdateUI += UpdateInventorySprite;
        Health.HPChanged += CheckHpBar;
        Item.UpdateUi += CheckHpBar;
        Item.UpdateUi += CheckEnergyBar;
    }

    private void OnDestroy()
    {
        Inventory.UpdateUI -= UpdateInventorySprite;
        Health.HPChanged -= CheckHpBar;
        Item.UpdateUi -= CheckHpBar;
        Item.UpdateUi -= CheckEnergyBar;
    }

    public void UpdateInventorySprite(int dimensionIndex, int slotIndex, bool itemUsed)
    {
        switch(dimensionIndex)    
        {
            case (0):
                UpdateInventorySprite(HealPotionSlots,slotIndex,itemUsed);
                break;
            case (1):
                UpdateInventorySprite(EnergyPotionSlots, slotIndex, itemUsed);
                break;
        }
    }

    private void UpdateInventorySprite(Image[] ItemImage, int slotIndex,bool itemUsed)
    {
        if (itemUsed)
        {
            ItemImage[slotIndex].sprite = emptyInventorySprite;
        }
        else
        {
            ItemImage[slotIndex].sprite = fullInventorySprite;
        }
    }

    public void CheckHpBar()
    {
        hpBar.fillAmount = player.GetComponent<Health>().Hp / 100;
    }
    
    public void CheckEnergyBar()
    {
        energyBar.fillAmount = player.GetComponent<Energy>().CurrentEnergy / 100;
    }

    public void AxeThrowCooldownSprite()
    {
        StartCoroutine(UpdateAbilityCooldownSprite(axeThrowImage,configs.axeThrowCooldown));
    }
    
    public void MightyPunchCooldownSprite()
    {
        StartCoroutine(UpdateAbilityCooldownSprite(mightyPunchImage,configs.mightyPunchCooldown));
    }    
    
    public void AirAtackCooldownSprite()
    {
        StartCoroutine(UpdateAbilityCooldownSprite(airAtackImage,configs.airAtackCooldown));
    }    

    public void DashCooldownSprite()
    {
        StartCoroutine(UpdateAbilityCooldownSprite(dashImage, configs.dashCooldown));
    }
    
    private IEnumerator UpdateAbilityCooldownSprite(Image abilityImage, float abilityCooldown)
    {
        abilityImage.fillAmount = 0;
        float i = 0f;
        while(i < abilityCooldown)
        {
            i += Time.deltaTime;
            abilityImage.fillAmount = i / abilityCooldown;
            yield return new WaitForSeconds(Time.deltaTime/2);
        }
    }
}
