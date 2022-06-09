using UnityEngine;
using UnityEngine.UI;

public class UnlockSystemUI : MonoBehaviour
{
    [Header("MightyPunch")]
    [SerializeField] private Image blockImageForMightyPunch;

    [Header("AirAtack")]
    [SerializeField] private Image blockImageForAirAtack;

    public void UpdateUi()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        if(blockImageForAirAtack.fillAmount > 0)
        {
            blockImageForAirAtack.fillAmount -= 0.5f;
        }
        else
        {
            blockImageForMightyPunch.fillAmount -= 0.5f;
        }
    }
}
