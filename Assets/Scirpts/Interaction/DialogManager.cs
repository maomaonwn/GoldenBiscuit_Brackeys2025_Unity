using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

/// <summary>Control the display of the dialog messages</summary>
public class DialogManager:MonoBehaviour
{
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
 
    private Queue<DialogLine> lines;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;
    public static DialogManager Instance;
    
    private DialogTrigger dialogTrigger;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        lines = new Queue<DialogLine>();
    } 
 
    public void StartDialogue(DialogObject dialog,DialogTrigger trigger)
    {
        isDialogueActive = true;
        dialogTrigger = trigger;//reference the trigger
        Time.timeScale = 0f;  // 暂停游戏时间

        animator.updateMode = AnimatorUpdateMode.UnscaledTime; // 让UI动画仍然播放
        animator.Play("Show");
 
        lines.Clear();
 
        foreach (DialogLine dialogLine in dialog.dialogueLines)
        {
            lines.Enqueue(dialogLine);
        }
 
        DisplayNextDialogueLine();
    }
 
    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        DialogLine currentLine = lines.Dequeue();
 
        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;
 
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(currentLine));
    }
 
    IEnumerator TypeSentence(DialogLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // 用Realtime
        }
    }
 
    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("Hide");
        Time.timeScale = 1f;  // 恢复游戏时间
        dialogTrigger.gameObject.SetActive(false);
    }
}