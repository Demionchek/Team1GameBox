using UnityEngine;
using UnityEngine.UI;

public class AirAtackUI : MonoBehaviour
{
    [SerializeField] private Image slot;
    [SerializeField] private Sprite atackSprite;
    [SerializeField] private Sprite airAtackSprite;
    [SerializeField] private AirAtack player;

    private AnimatorManager animatorManager;

    private void Start()
    {
        if (player.gameObject.TryGetComponent<AnimatorManager>(out AnimatorManager manager))
            animatorManager = manager;
    }

    private void Update()
    {
        CheckUiChagne();
    }

    private void CheckUiChagne()
    {
        if (!animatorManager.isGrounded())
            UpdateAtackIcon();
        else if (slot.sprite != atackSprite)
            slot.sprite = atackSprite;
    }

    public void UpdateAtackIcon()
    {
        if (player.AirAtackAvailable())
            slot.sprite = airAtackSprite;
        else
            slot.sprite = atackSprite;
    }
}
