using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>单句对话的数据结构</summary>
[Serializable]
public class DialogLine
{
    public DialogCharacter character; 
    [TextArea(3, 10)]
    public string line;
}

/// <summary>承载整段对话的SO</summary>
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogObject : ScriptableObject
{
    public List<DialogLine> dialogueLines = new List<DialogLine>();
}