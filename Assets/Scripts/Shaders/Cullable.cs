using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cullable : MonoBehaviour
{
    //speed of the transition
    public float alphaChangeSpeed = 3f;

    // name of the variable in the shader that will be adjusted
    public string shaderVariableName = "Alpha";

    // end point of culling
    public float fadeTo = 0;
    // start point of culling
    public float fadeFrom = 1;

    // the float for the transition
    private float currentAlpha = 1.0f;

    // The material that will be affected by the transition 
    private Material mat;
    // Set to true if we want to fade the object out because it's in the way
    private bool occluding;

    // Set to true when the fade control co-routine is active
    private bool inCoroutine = false;


    public bool Occluding
    {
        get { return occluding; }
        set
        {
            if (occluding != value)
            {
                occluding = value;
                OnOccludingChanged();
            }
        }

    }

    void Start()
    {
        // grab the renderer's material and set the current alpha 
        mat = GetComponent<Renderer>().material;
        currentAlpha = fadeFrom;
    }

    // Called when the Occluding value is changed
    private void OnOccludingChanged()
    {
        if (!inCoroutine)
        {
            StartCoroutine(FadeAlphaRoutine());
            inCoroutine = true;
            Debug.Log("Started OnOccludingChanged");
        }
    }

    private IEnumerator FadeAlphaRoutine()
    {
        while (currentAlpha != GetTargetAlpha())
        {
            float alphaShift = alphaChangeSpeed * Time.deltaTime;

            float targetAlpha = GetTargetAlpha();
            if (currentAlpha < targetAlpha)
            {
                currentAlpha += alphaShift;
                if (currentAlpha > targetAlpha)
                {
                    currentAlpha = targetAlpha;
                }
            }
            else
            {
                currentAlpha -= alphaShift;
                if (currentAlpha < targetAlpha)
                {
                    currentAlpha = targetAlpha;
                }
            }

            /*m_mat.color = new Color(m_mat.color.r, m_mat.color.g, m_mat.color.b, m_currentAlpha); */
            mat.SetFloat(shaderVariableName, currentAlpha);

            yield return null;
        }
        inCoroutine = false;
    }

    // Return the alpha value we want on all of our models
    private float GetTargetAlpha()
    {
        if (occluding)
        {
            return fadeTo;
        }
        else
        {
            return fadeFrom;
        }
    }
}



