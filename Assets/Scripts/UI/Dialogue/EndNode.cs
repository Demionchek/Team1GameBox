using UnityEngine;

public class EndNode : BaseNode
{
    [Input] public int entry;

    [SerializeField] private DialogueGrapgh nextGraph;

    public override string GetString()
    {
        return "End";
    }

    public override DialogueGrapgh GetGrapgh()
    {
        return nextGraph;
    }
}
