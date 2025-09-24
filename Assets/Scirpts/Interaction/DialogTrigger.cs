using UnityEngine;

namespace Scirpts.Interaction
{
    public enum DialogTriggerMode
    {
        AutoOnEnter,  // 进入trigger范围之后就触发
        PressKey    // 按键触发
    }

    /// <summary>Reference dialog data, Send the data to Dialog manager to display</summary>
    public class DialogTrigger : MonoBehaviour
    {
        public DialogObject dialog;

        public DialogTriggerMode mode = DialogTriggerMode.AutoOnEnter;

        public bool triggerOnce = true;
    
        private bool hasTriggered = false;
 
        public void TriggerDialogue()
        {
            if (hasTriggered) return;
            DialogManager.Instance.StartDialogue(dialog,this);
            //Set to true if can only be triggered once
            if (triggerOnce) hasTriggered = true;

        }
        // AutoOnEnter
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (mode != DialogTriggerMode.AutoOnEnter) return;
            if(collision.CompareTag("Player"))
            {
                Debug.Log("Player entered");
                TriggerDialogue();
            }
        }
    }
}