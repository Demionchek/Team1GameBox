using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Line")]

public class NarrationLine : ScriptableObject
{
    [SerializeField] private NarrationCharacter speaker;
    [SerializeField] private string text;

    public NarrationCharacter Speaker => speaker;

    public string Text => text;
}
