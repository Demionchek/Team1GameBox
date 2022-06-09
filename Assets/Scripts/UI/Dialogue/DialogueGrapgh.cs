using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGrapgh : NodeGraph 
{
	public BaseNode current;

	public void TryFindStartNode()
    {
        try
        {
            foreach(var node in this.nodes)
            {
                if (node.name == "Start")
                    current = (BaseNode) node;
            }
        }
        catch
        {
            Debug.Log("DIALOGUE GRAPH TRYFINDSTARTNODE EXCEPTION");
        }
            
    }
}