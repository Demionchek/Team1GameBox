using StarterAssets;
using System.Collections;
using UnityEngine;

public class AxeReturn : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private float timeToRelocateAfterCollision;
    [SerializeField] private Transform playersHand;
    [SerializeField] private LayerMask layersToIgnore;
    [SerializeField] private float timeToReturn;

    private int counter = 0;
    private Rigidbody rigidBody;
    public bool isActive { get; set; }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isActive)
            StartCoroutine(ReturnCoroutine());
    }

    private IEnumerator ReturnCoroutine()
    {
        yield return new WaitForSeconds(timeToReturn);
        StartCoroutine(ReturnAxe());
    }

    private IEnumerator ReturnAxe()
    {
        yield return new WaitForSeconds(timeToRelocateAfterCollision);
        counter = 0;
        gameObject.SetActive(false);
        gameObject.transform.parent = playersHand;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        isActive = false;
    }    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller)
            && other.gameObject.layer != layersToIgnore)
        {
            rigidBody.isKinematic = true;
            if (other.transform.TryGetComponent(out IDamageable damageable) && counter == 0)
            {
                gameObject.transform.parent = other.transform;
                counter++;
                damageable.TakeDamage((int)configs.axeThrowDmg, configs.enemyLayer);
            }    
        }
        StartCoroutine(ReturnAxe());
    }
}
