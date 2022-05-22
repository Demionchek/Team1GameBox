using System.Collections;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(YagaSummons))]
public class SpellCaster : MonoBehaviour
{
    [SerializeField] private AttackMarkersController attackMarkers;
    [Range(0, 1), SerializeField] private float _summonChance = 0.5f;

    private Material material;
    private YagaSummons summons;

    private int _lastSpell;
    private const int _clowdSpell = 0;
    private const int _multyConeSpell = 1;
    private const int _pondSpell = 2;
    private const int _ConesNPondSpell = 3;

    private const int _spellCount = 4;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        summons = GetComponent<YagaSummons>();
    }

    public void GetNewRandomSpell(ThirdPersonController controller ,float timeToDel)
    {
        int r = Random.Range(0, _spellCount);
        while (r == _lastSpell) r = Random.Range(0, _spellCount);
        _lastSpell = r;
        switch (_lastSpell)
        {
            case _clowdSpell:
                StartCoroutine(CloudCorutine(controller, timeToDel));
                ChanceOfSummon(controller, timeToDel);
                break;
            case _pondSpell:
                material.color = Color.blue;
                attackMarkers.CreateBossPondSpell(transform.position, timeToDel);
                ChanceOfSummon(controller, timeToDel);
                break;
            case _ConesNPondSpell:
                material.color = Color.black;
                attackMarkers.CreateBossPondSpell(transform.position, timeToDel);
                attackMarkers.CreateMultyCones(transform.position, timeToDel);
                break;
            case _multyConeSpell:
                material.color = Color.red;
                attackMarkers.CreateMultyCones(transform.position, timeToDel);
                ChanceOfSummon(controller, timeToDel);
                break;
        }
    }

    private IEnumerator CloudCorutine( ThirdPersonController controller, float time)
    {
        material.color = Color.green;
        yield return new WaitForSeconds(time);
        attackMarkers.CreateClowdSpell(controller, transform.position);
    }

    private void ChanceOfSummon(ThirdPersonController controller, float timeToDel)
    {
        float r = Random.value;
        if (r < _summonChance)
        {
            summons.GetNewRandomSummon(controller, timeToDel);
        }
    }
}
