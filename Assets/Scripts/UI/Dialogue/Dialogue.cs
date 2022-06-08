using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Dialogue")]

public class Dialogue : ScriptableObject
{
    [SerializeField] private DialogueNode firstNode;

    public DialogueNode FirstNode => firstNode;
}
