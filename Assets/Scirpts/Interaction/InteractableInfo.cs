using UnityEngine;

public class InteractableInfo : MonoBehaviour
{
    [TextArea] public string description; 
    public float interactDistance = 2.5f;
    public string prompt = "Press E to interact";
}