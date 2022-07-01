using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    [SerializeField] private GameObject[] _packs;
    [SerializeField] private int nextCheckPoint;
    private bool _spawn;

    private void Start()
    {
        SavingSystem savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        savingSystem.LoadWorldData(ref worldData);
        int i = worldData.CheckPoint;
        if (i < nextCheckPoint)
        {
            _spawn = true;
            foreach (var pack in _packs)
            {
                pack.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            if (_spawn)
            {
                foreach (var pack in _packs)
                {
                    pack.SetActive(true);
                }
                _spawn = false;
            }
        }
    }
}
