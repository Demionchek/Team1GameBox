using UnityEngine;

public class DialogueNode : BaseNode 
{
    [Input] public int entry;
    [Output] public int exit;

    public string speakerName;
    public string dialogueLine;
    public Sprite speakerImage;
    //public RectTransform backGroundImage;
    //public float backHeight;

    //public override float GetHeight()
    //{
    //    return backHeight;
    //}

    //public override RectTransform GetRect()
    //{
    //    return backGroundImage;
    //}

    public override string GetString()
    {
        return "DialogueNode/" + speakerName + "/" + dialogueLine ;
    }

    public override Sprite GetSprite()
    {
        return speakerImage;
    }
}