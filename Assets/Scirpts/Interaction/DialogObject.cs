using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>Store dialog lines</summary>
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]

[System.Serializable]
public class DialogLine
{
    public DialogCharacter character;
    [TextArea(3, 10)]
    public string line;
}

public class DialogObject : ScriptableObject
{
    public List<DialogLine> dialogueLines = new List<DialogLine>();
}

