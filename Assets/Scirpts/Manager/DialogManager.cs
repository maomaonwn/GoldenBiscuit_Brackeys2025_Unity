using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager I;

    [Header("Prefabs")]
    public Dialog promptPrefab;      // 提示用气泡
    public Dialog objDialogPrefab;   // 物体对白
    public Transform worldRoot;

    [Header("Player Bubble")]
    public Dialog playerDialogPrefab;
    public Transform player;

    void Awake()
    {
        if (I && I != this) { Destroy(gameObject); return; }
        I = this;
        if (!worldRoot) worldRoot = transform;
    }
    
    
    public void Play(Transform target, InteractableInfo info)
    {
        StopAllCoroutines();
        StartCoroutine(CoPlay(target, info));
    }

    private IEnumerator CoPlay(Transform target, InteractableInfo info)
    {
        // Cue
        var objDialog = Instantiate(objDialogPrefab, worldRoot);
        objDialog.Setup(target, info.objectLine);
        yield return new WaitForSeconds(info.objectDuration);
        Destroy(objDialog.gameObject);

        // Wait for sec
        yield return new WaitForSeconds(info.replyDelay);

        // Player dialog
        if (player && playerDialogPrefab && !string.IsNullOrEmpty(info.playerReply))
        {
            var pBubble = Instantiate(playerDialogPrefab, worldRoot);
            pBubble.Setup(player, info.playerReply);
            yield return new WaitForSeconds(info.playerDuration);
            Destroy(pBubble.gameObject);
        }
    }
    
    public Dialog ShowPrompt(Transform target, string text)
    {
        if (!promptPrefab) return null;
        var promptDialog = Instantiate(promptPrefab, worldRoot);
        promptDialog.Setup(target, text);
        return promptDialog;
    }
    
}
