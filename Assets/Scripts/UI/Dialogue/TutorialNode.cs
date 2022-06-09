using UnityEngine;

public class TutorialNode : BaseNode 
{
    [Input] public int entry;
    [Output] public int exit;

    public string tutorialLine;
    public Sprite sprite;

    public override string GetString()
    {
        return "Tutorial/" + tutorialLine;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }
}