using System.Collections;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private float timeToSpawnAfterDeath;

    private Health health;
    private bool isDead { get { return health.Hp > 1; } }
    private int counter;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (isDead && counter==0)
            StartCoroutine(SpawnDrop());
    }

    private IEnumerator SpawnDrop()
    {
        counter++;
        yield return new WaitForSeconds(timeToSpawnAfterDeath);
        Instantiate(drop,transform.position,Quaternion.identity);
    }
}
