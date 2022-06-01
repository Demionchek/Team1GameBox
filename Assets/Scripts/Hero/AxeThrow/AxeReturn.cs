using StarterAssets;
using System.Collections;
using UnityEngine;

public class AxeReturn : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private float timeToRelocateAfterCollision;
    [SerializeField] private Transform playersHand;
    [SerializeField] private LayerMask playersLayer;

    private int counter = 0;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playersLayer)
        {
            rigidBody.isKinematic = true;
            if (other.transform.TryGetComponent(out IDamageable damageable) && counter == 0)
            {
                gameObject.transform.parent = other.transform;
                counter++;
                damageable.TakeDamage(configs.axeThrowDmg, configs.enemyLayer);
            }
            StartCoroutine(ReturnAxe());
        }
    }

    private IEnumerator ReturnAxe()
    {
        yield return new WaitForSeconds(timeToRelocateAfterCollision);
        counter = 0;
        gameObject.SetActive(false);
        gameObject.transform.parent = playersHand;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
