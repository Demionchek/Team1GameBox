using StarterAssets;
using UnityEngine;
using System.Collections;

public class YagaSummons : MonoBehaviour
{
    [SerializeField] private AttackMarkersController _attackMarkers;
    [Header("Minions")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private int _enemiesCount; 
    [Header("Hut")]
    [SerializeField] private GameObject _hutOnLegs;
    [SerializeField] private float _hutSpeed;
    [SerializeField] private float _hutRunTime;
    [SerializeField] private Transform _rotator;
    [Header("Rockets")]
    [SerializeField] private float _rocketsDelay = 0.5f;
    [SerializeField] private int _rocketsCount = 4;

    private int _lastSummon;
    private const int _rocketsSummon = 0;
    private const int _hutSummon = 1;
    private const int _minionsSummon = 2;
    private const int _summonsCount = 3;
    private Rigidbody _hutRb;


    private void Start()
    {
        _hutRb = _hutOnLegs.GetComponent<Rigidbody>();
    }

    public void GetNewRandomSummon(ThirdPersonController controller, float timeToDel)
    {
        int r = Random.Range(0, _summonsCount);
        while (r == _lastSummon) r = Random.Range(0, _summonsCount);
        _lastSummon = r;
        switch (_lastSummon)
        {
            case _rocketsSummon:
                StartCoroutine(RocketsDelayCor(_rocketsDelay, controller, timeToDel));
                break;
            case _minionsSummon:
                _enemySpawner.EnemySummon(_enemiesCount);
                break;
            case _hutSummon:
                StartCoroutine(HutOnLegsRun(timeToDel, controller));
                break;
        }
    }

    private IEnumerator HutOnLegsRun(float delay, ThirdPersonController controller)
    {
        _rotator.Rotate(0, Random.Range(0, 360),0);
        Vector3 pos = _rotator.GetChild(0).position;
        _attackMarkers.CreateHutRaySpell(pos, controller.transform.position, delay);
        Vector3 lookDir = controller.transform.position;
        yield return new WaitForSeconds(delay);
        _hutOnLegs.transform.position = pos;
        lookDir.y = 0;
        _hutOnLegs.transform.LookAt(lookDir);
        Vector3 forceVector = lookDir - pos;
        _hutRb.AddForce(forceVector.normalized * _hutSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(_hutRunTime);
        _hutRb.velocity = Vector3.zero;
    }

    private IEnumerator RocketsDelayCor(float delay, ThirdPersonController controller, float timeToDel)
    {
        int counter = 0;
        while (counter <= _rocketsCount)
        {
            yield return new WaitForSeconds(delay);
            _attackMarkers.CreateEnemyPondMarker(controller.transform.position, timeToDel);
            counter++;
        }
    }
}
