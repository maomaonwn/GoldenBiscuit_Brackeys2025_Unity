using System.Collections.Generic;
using UnityEngine;

/// <summary>Reference dialog data, Send the data to Dialog manager to display</summary>
public class DialogTrigger : MonoBehaviour
{
    public DialogObject dialog;
    
    private bool hasTriggered = false;
 
    private void TriggerDialogue()
    {
        if (hasTriggered) return;
        DialogManager.Instance.StartDialogue(dialog,this);
        hasTriggered = true;

    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            TriggerDialogue();
        }
    }
}