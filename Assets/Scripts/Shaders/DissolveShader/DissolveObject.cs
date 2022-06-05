using System.Collections;
using UnityEngine;

public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float dissolveTime = 2f;

    private Material material;
    private float dissolveSpeed;
    private readonly string dissolveParameterName = "CutoffHeigth";
    
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        dissolveSpeed = material.GetFloat(dissolveParameterName) / dissolveTime;
    }

    public void StartDissolve()
    {
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        float parameterValue = material.GetFloat(dissolveParameterName);
        for (float i = 0; i < dissolveTime; i += Time.deltaTime, parameterValue -= dissolveSpeed)
        {
            yield return new WaitForSeconds(Time.deltaTime/2);
            material.SetFloat(dissolveParameterName, parameterValue);
        }
    }
}
