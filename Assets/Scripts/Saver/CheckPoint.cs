using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Saver _saver;
    [SerializeField] private VisualEffect _visualEffect;
    private bool _isActivated;
    [SerializeField] public int _pointNumber;
    [SerializeField] public Transform _spawnPoint;
    [SerializeField] private Color _emissionColor;
    [SerializeField] private float _emissionAmount;
    public int PointNumber { get { return _pointNumber; } }
    public Transform SpawnPoint { get { return _spawnPoint; } }
    private void Start()
    {
        if (_saver.CheckPointToSave < PointNumber)
        {
            _isActivated = false;
            _visualEffect.Stop();
        }
        else
        {
            _isActivated = true;
            _visualEffect.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController controller))
        {
                int scene = SceneManager.GetActiveScene().buildIndex;
                _saver.SaveLevel(scene);
            if (!_isActivated)
            {
                _visualEffect.Play();
                var inventory = controller.GetComponent<Inventory>();
                var health = controller.GetComponent<Health>();
                var energy = controller.GetComponent<Energy>();
                int hPacks = inventory.GetCountOfItem(ItemType.HealthPotion);
                int ePacks = inventory.GetCountOfItem(ItemType.EnergyPotion);
                _saver.SaveCheckPoint(PointNumber);
                _saver.SaveHealth((int)health.Hp);
                _saver.SaveEnergy((int)energy.CurrentEnergy);
                _saver.SaveHealthPacks(hPacks);
                _saver.SaveEnergyPacks(ePacks);
                _isActivated = true;
            }
        }
    }
}
