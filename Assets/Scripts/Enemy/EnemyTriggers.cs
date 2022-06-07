using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    [SerializeField] private GameObject[] _packs;
    [SerializeField] private Saver _saver;
    [SerializeField] private int nextCheckPoint;
    private bool _spawn;

    private void Start()
    {
        _saver.LoadCheckPoint();
        int i = _saver.CheckPointToSave;
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
