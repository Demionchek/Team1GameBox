using System.Collections;
using UnityEngine;

public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float dissolveTime = 2f;

    private Material material;
    private float dissolveSpeed;
    private readonly string dissolveParameterName = "_CutoffHeigth";
    
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        dissolveSpeed = material.GetFloat(dissolveParameterName) /dissolveTime*Time.fixedDeltaTime;
    }

    public void StartDissolve()
    {
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        float parameterValue = material.GetFloat(dissolveParameterName);
        while(parameterValue > 0)
        {
            parameterValue -= dissolveSpeed;
            material.SetFloat(dissolveParameterName, parameterValue);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StopCoroutine(Dissolve());
    }
}
