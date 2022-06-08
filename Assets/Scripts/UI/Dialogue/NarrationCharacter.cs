using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Character")]

public class NarrationCharacter : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Image characterImage;

    public string CharacterName => characterName;

    public Image CharacterImage => characterImage;
}
