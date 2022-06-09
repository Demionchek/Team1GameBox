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
        if(blockImageForMightyPunch.fillAmount > 0)
        {
            blockImageForMightyPunch.fillAmount -= 0.5f;
        }
        else
        {
            blockImageForAirAtack.fillAmount -= 0.5f;
        }
    }
}
