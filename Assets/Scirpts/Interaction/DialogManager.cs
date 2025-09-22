using System.Collections;
using System.Collections.Generic;
using Scirpts.EntityStates.BossControl;
using Scirpts.PlayerControl;
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

    [Header("参与对话的实体")]
    public Player player;
    public Boss boss;

    public bool b_InDialog = false;
    
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
        // Time.timeScale = 0f;  // 暂停游戏时间    //在对话结束后会卡顿一次，目前停止使用timeScale来修复问题
        player.machine.DisableAllStates();  //禁用玩家所有状态
        boss.machine.DisableAllStates();  //禁用Boss所有状态

        animator.updateMode = AnimatorUpdateMode.UnscaledTime; // 让UI动画仍然播放
        animator.Play("Show");
 
        lines.Clear();
 
        foreach (DialogLine dialogLine in dialog.dialogueLines)
        {
            lines.Enqueue(dialogLine);
        }

        b_InDialog = true;
        
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
        // Time.timeScale = 1f;  // 恢复游戏时间
        player.machine.EnableStateMachine(player.idleState);  //重启玩家的状态机
        boss.machine.EnableStateMachine(boss.idleState);  //重启Boss的状态机
        //dialogTrigger.gameObject.SetActive(false);

        b_InDialog = false;
    }
}