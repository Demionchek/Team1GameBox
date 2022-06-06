using UnityEngine;
using UnityEngine.VFX;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private VisualEffect _firstSlashEffect;
    [SerializeField] private VisualEffect _secondSlashEffect;
    [SerializeField] private VisualEffect _thirdlashEffect;
    [SerializeField] private VisualEffect _dashEffect;
    [SerializeField] public VisualEffect GroundSlam;

    private void Start()
    {
        _firstSlashEffect.Stop();
        _secondSlashEffect.Stop();
        _thirdlashEffect.Stop();
        _dashEffect.Stop();
    }

    public void PlayDashEffect()
    {
        _dashEffect.Play();
    }

    public void StopDashEffect()
    {
        _dashEffect.Stop();
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
