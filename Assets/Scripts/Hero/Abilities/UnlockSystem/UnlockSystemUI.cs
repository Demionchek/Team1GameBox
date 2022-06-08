using UnityEngine;
using UnityEngine.UI;

public class UnlockSystemUI : MonoBehaviour
{
    [Header("MightyPunch")]
    [SerializeField] private Image blockImageForMightyPunch;

    [Header("AirAtack")]
    [SerializeField] private Image blockImageForAirAtack;
    [SerializeField] private GameObject airAtackUnlockUi;

    public void UpdateUi()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        if(blockImageForAirAtack.fillAmount > 0)
        {
            blockImageForAirAtack.fillAmount -= 0.5f;
            if (blockImageForAirAtack.fillAmount == 0)
                airAtackUnlockUi.SetActive(false);
            return;
        }
        else
        {
            blockImageForMightyPunch.fillAmount -= 0.5f;
        }
    }
}
