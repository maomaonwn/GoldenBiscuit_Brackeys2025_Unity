using UnityEngine;

namespace Scirpts.Interaction
{
    public class InteractableInfo : MonoBehaviour
    {
        [Header("Interact")]
        public float interactDistance = 2.5f;
        [TextArea] public string promptLine = "Press F to interact";
    }
}