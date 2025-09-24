using UnityEngine;

namespace Scirpts.Interaction
{
    /// <summary>Store dialog characters</summary>
    [CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Dialogue System/Character")]
    public class DialogCharacter : ScriptableObject
    {
        public string characterName;
        public Sprite icon;
    }
}
