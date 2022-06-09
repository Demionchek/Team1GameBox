using UnityEngine;
using XNode;

public class BaseNode : Node 
{
    public virtual float GetHeight()
    {
        return 0;
    }

    public virtual RectTransform GetRect()
    {
        return null;
    }

    public virtual string GetString()
    {
        return null;
    }

    public virtual Sprite GetSprite()
    {
        return null;
    }

    public virtual DialogueGrapgh GetGrapgh()
    {
        return null;
    }
}