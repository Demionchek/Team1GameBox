using System.Collections;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(YagaSummons))]
public class SpellCaster : MonoBehaviour
{
    [SerializeField] private AttackMarkersController _attackMarkers;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Renderer _waterRenderer;
    [SerializeField] private Transform _spawnPoint; 
    [Range(0, 1), SerializeField] private float _summonChance = 0.5f;

    private Material _material;
    private YagaSummons _summons;

    private int _lastSpell;
    private const int _clowdSpell = 0;
    private const int _multyConeSpell = 1;
    private const int _pondSpell = 2;
    private const int _conesNPondSpell = 3;

    private const int _spellCount = 4;

    private void Awake()
    {
        _material = _waterRenderer.material;
        _summons = GetComponent<YagaSummons>();
    }

    public void GetNewRandomSpell(ThirdPersonController controller ,float timeToDel)
    {
        int r = Random.Range(0, _spellCount);
        while (r == _lastSpell) r = Random.Range(0, _spellCount);
        _lastSpell = r;
        _spawner.EnemySummon(_summons.EnemiesCount);
        switch (_lastSpell)
        {
            case _clowdSpell:
                StartCoroutine(CloudCorutine(controller, timeToDel));
                ChanceOfSummon(controller, timeToDel);
                break;
            case _pondSpell:
                _material.color = Color.blue;
                _attackMarkers.CreateBossPondSpell(_spawnPoint.position, timeToDel);
                ChanceOfSummon(controller, timeToDel);
                break;
            case _conesNPondSpell:
                _material.color = Color.black;
                _attackMarkers.CreateBossPondSpell(_spawnPoint.position, timeToDel);
                _attackMarkers.CreateMultyCones(_spawnPoint.position, timeToDel);
                break;
            case _multyConeSpell:
                _material.color = Color.red;
                _attackMarkers.CreateMultyCones(_spawnPoint.position, timeToDel);
                ChanceOfSummon(controller, timeToDel);
                break;
        }
    }

    private IEnumerator CloudCorutine( ThirdPersonController controller, float time)
    {
        _material.color = Color.green;
        yield return new WaitForSeconds(time);
        _attackMarkers.CreateClowdSpell(controller, _spawnPoint.position);
    }

    private void ChanceOfSummon(ThirdPersonController controller, float timeToDel)
    {
        float r = Random.value;
        if (r < _summonChance)
        {
            _summons.GetNewRandomSummon(controller, timeToDel);
        }
    }
}
