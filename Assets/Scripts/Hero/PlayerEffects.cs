using UnityEngine;
using UnityEngine.VFX;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private VisualEffect _firstSlashEffect;
    [SerializeField] private VisualEffect _secondSlashEffect;
    [SerializeField] private VisualEffect _thirdlashEffect;

    private void Start()
    {
        _firstSlashEffect.Stop();
        _secondSlashEffect.Stop();
        _thirdlashEffect.Stop();
    }

    public void FirstSlashEffect()
    {
        _firstSlashEffect.Play();
    }

    public void SecondSlashEffect()
    {
        _secondSlashEffect.Play();
    }

    public void ThirdSlashEffect()
    {
        _thirdlashEffect.Play();
    }
}
